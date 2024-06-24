using MongoDB.Bson;

namespace NoteRepository.Components.Models
{
    public class NoteTitleDto
    {
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
    }
}
