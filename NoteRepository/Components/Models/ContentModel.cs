namespace NoteRepository.Components.Models
{
    public class ContentModel
    {
        public string Id { get; }
        public string Content { get; set; }
        public string originalContent { get; set; }
        public bool EditMode { get; set; }

        public ContentModel(string content) { 
            // Generate a unique ID.
            Id = Guid.NewGuid().ToString();
            Content = content;
            originalContent = content;
            // All Content objects begin not in edit mode.
            EditMode = false;
        }
    }
}
