namespace NoteRepository.Components.Models
{
    public class NotesModel
    {
        public List<NoteTitleDto> Notes { get; private set; }

        public NotesModel(List<NoteTitleDto> notes) {
            Notes = notes;
        }

        public void DeleteNote(NoteTitleDto note)
        {
            // Delete note from local list.
            Notes.Remove(note);
        }

        public void CreateNote(string name)
        {
            var note = new NoteModel(name);
            var noteTitleDto = new NoteTitleDto { Id = note.Id, Name = name };

            Notes.Add(noteTitleDto);
        }
    }
}
