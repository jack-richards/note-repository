namespace NoteRepository.Components.Models
{
    public class ContentModel
    {
        public string Id { get; }
        public string Content { get; set; }
        private string OriginalContent;
        public bool EditMode { get; private set; }

        public ContentModel(string content)
        {
            // Generate a unique ID.
            Id = Guid.NewGuid().ToString();
            Content = content;
            OriginalContent = content;
            // All Content objects begin not in edit mode.
            EditMode = false;
        }

        public void ToggleEditMode()
        {
            // Set value to opposite of what it currently is, e.g. true => false.
            EditMode = !EditMode;
        }

        public void CancelChanges()
        {
            // Revert Content/changes to original value.
            Content = OriginalContent;
            // Exit edit mode. Maybe after deleting changes? Depends on how I implement the editing logic.
            ToggleEditMode();
        }

        public void SaveChanges()
        {
            // Change original content field to reflect changes, new baseline.
            OriginalContent = Content;
            // Exit edit mode.
            ToggleEditMode();
        }

        public bool ChangesMade()
        {
            if(Content != OriginalContent)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
