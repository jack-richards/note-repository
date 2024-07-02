using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoteRepository.Components.Models
{
    [BsonIgnoreExtraElements]
    public class NoteDto
    {
        [BsonId]
        public ObjectId Id { get; set; }
        // Each note should at the very least be given a name.
        public required string Name { get; set; }
        public string[] Content { get; set; } = [];
        public string[] ListLinks { get; set; } = [];
    }
}
