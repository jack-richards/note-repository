namespace NoteRepository.MongoDatabase;

using MongoDB.Driver;
using System;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using NoteRepository.Components.Models;
using System.Collections.Generic;
using MongoDB.Bson;

public class MongoDbService
{
    private readonly IMongoCollection<NoteModel> _NotesCollection;
    private readonly ILogger<MongoDbService> _logger;

    public MongoDbService(IOptions<MongoDBSettings> settings, ILogger<MongoDbService> logger)
    {
        var connectionString = settings.Value.ConnectionString;
        _logger = logger;

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "Mongodb connection string not set within appsettings.json");
        }

        var client = new MongoClient(connectionString);
        var databaseName = new MongoUrl(connectionString).DatabaseName;

        if (string.IsNullOrEmpty(databaseName))
        {
            throw new ArgumentNullException(nameof(databaseName));
        }

        var database = client.GetDatabase(databaseName);

        if (!CollectionExists("lists", database))
        {
            database.CreateCollection("lists");
        }

        _NotesCollection = database.GetCollection<NoteModel>("lists");
    }
    public async Task<List<NoteTitleDto>> GetNoteTitlesAsync()
    {
        // Use a projection to return only the Id and Name of each note.
        // This is to reduce memory usage of the Notes page.
        var projection = Builders<NoteModel>.Projection.Expression(note => new NoteTitleDto
        {
            Id = note.Id,
            Name = note.Name
        });

        var notes = await _NotesCollection.Find(_ => true)
            .Project(projection)
            .ToListAsync();

        return notes;
    }

    public async Task<NoteModel> GetNoteByNameAsync(string name)
    {
        // Find the first 'Note' that matches name.
        return await _NotesCollection.Find(n => n.Name == name)
            .FirstOrDefaultAsync();
    }

    public async Task deleteNote(NoteTitleDto note)
    {
        await _NotesCollection.DeleteOneAsync<NoteModel>(n => n.Id == note.Id);
    }

    public async Task saveNote(NoteModel note, List<ContentModel> contentModels)
    {
        // Iterate over ContentModel list and create array of contents, string[].
        var contents = contentModels.Select(c => c.Content).ToArray();
        // Update the note with any changes to content.
        var updateDefinition = Builders<NoteModel>.Update.Set(n => n.Content, contents);
        await _NotesCollection.UpdateOneAsync(n => n.Id == note.Id, updateDefinition);
    }

    private bool CollectionExists(string collectionName, IMongoDatabase database)
    {
        var filter = new BsonDocument("name", collectionName);
        // Creates a cursor that filters the results by the specified name.
        var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
        // Determines if any results were returned, whether the collection exists or not.
        return collections.Any();
    }
}
