using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoteRepository.Components.Models
{
    [BsonIgnoreExtraElements]
    public class NoteModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string[] Content { get; set; }
        public required string[] listLinks { get; set; }
    }
}
