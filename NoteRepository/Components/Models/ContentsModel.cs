using NoteRepository.MongoDatabase;

namespace NoteRepository.Components.Models
{
    public class ContentsModel
    {
        public List<ContentModel> Contents { get; private set; }

        public ContentsModel()
        {
            Contents = [];
        }

        public ContentsModel(string[] Content)
        {
            // Go through each Content element in the note and convert each into a ContentModel.
            // Storing the result within the Contents field.
            this.Contents = Content.Select((content, index) =>
                new ContentModel(
                    content
                )).ToList();
        }

        public void DeleteContent(ContentModel content)
        {
            // Delete content object from local dictionary.
            Contents.Remove(content);
            // Persist changes in database.
            //await _mongoDbService.saveNote(_note, this);
        }

        public void CreateContent()
        {
            var content = new ContentModel("");
            Contents.Add(content);
            //await _mongoDbService.saveNote(_note, this);
        }
    }
}
