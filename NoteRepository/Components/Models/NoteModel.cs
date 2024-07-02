using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoteRepository.Components.Models
{
    [BsonIgnoreExtraElements]
    public class NoteModel
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        // Each note should at the very least be given a name.
        public string Name { get; private set; }
        public ContentsModel ContentsModel { get; private set; }
        public string[] ListLinks { get; private set; } = [];

        public NoteModel(string name)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            ContentsModel = new ContentsModel();
        }

        public NoteModel(ObjectId id, string name, string[] content, string[] listLinks)
        {
            Id = id;
            Name = name;
            ContentsModel = new ContentsModel(content);
            ListLinks = listLinks;
        }
    }
}
