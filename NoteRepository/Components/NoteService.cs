using NoteRepository.Components.Models;
using NoteRepository.MongoDatabase;

namespace NoteRepository.Components
{
    public class NoteService
    {
        private readonly MongoDbService _mongoDbService;

        // Various events used by other components to signal when to re-render.
        public event EventHandler? NoteCreated;
        public event EventHandler? NoteDeleted;
        public event EventHandler? NoteSaved;
        public event EventHandler? ChangesCancelled;

        public NoteService(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public void OnChangesCancelled()
        {
            ChangesCancelled?.Invoke(this, EventArgs.Empty);
        }

        public async Task SaveNoteAsync(NoteModel note)
        {
            // Convert NoteModel to NoteDto.
            var noteDto = ConvertNoteToDto(note);
            await _mongoDbService.SaveNoteAsync(noteDto);
            // Emit event
            NoteSaved?.Invoke(this, EventArgs.Empty);
        }

        public async Task DeleteNoteAsync(NoteTitleDto note)
        {
            await _mongoDbService.DeleteNoteAsync(note);
            NoteDeleted?.Invoke(this, EventArgs.Empty);
        }

        public async Task CreateNoteAsync(string noteTitle)
        {
            var noteDto = new NoteDto { Name = noteTitle };
            await _mongoDbService.CreateNoteAsync(noteDto);
            NoteCreated?.Invoke(this, EventArgs.Empty);
        }

        public async Task<NoteModel> GetNoteByNameAsync(string name)
        {
            var noteDto = await _mongoDbService.GetNoteByNameAsync(name);
            var noteModel = new NoteModel(noteDto.Id, noteDto.Name, noteDto.Content, noteDto.ListLinks);
            return noteModel;
        }

        public async Task<NotesModel> GetNoteTitlesAsync()
        {
            var noteTitleDtos = await _mongoDbService.GetNoteTitlesAsync();
            return new NotesModel(noteTitleDtos);
        }

        private static NoteDto ConvertNoteToDto(NoteModel note)
        {
            // Convert NoteModel to NoteDto.
            var noteDto = new NoteDto
            {
                Id = note.Id,
                Name = note.Name,
                // While a little messy will iterate through each ContentModel, extracting the content of each into an Array.
                Content = note.ContentsModel.Contents
                .Select(c => c.Content)
                .ToArray(),
                ListLinks = note.ListLinks
            };

            return noteDto;
        }
    }
}
