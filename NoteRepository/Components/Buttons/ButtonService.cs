namespace NoteRepository.Components.Buttons
{
    public class ButtonService
    {
        public Type ?ButtonComponentType { get; private set; }
        public Dictionary<string, object> ButtonComponentParameters { get; private set; } = new Dictionary<string, object>();

        public event EventHandler? ButtonChanged;

        public event EventHandler? CreateNotePressed;

        public void ChangeButton(Type ButtonType, Dictionary<string, object> ButtonComponentParameters)
        {
            ButtonComponentType = ButtonType;
            this.ButtonComponentParameters = ButtonComponentParameters;
            ButtonChanged?.Invoke(this, EventArgs.Empty);
        }

        public void OnCreateNotePressed()
        {
            CreateNotePressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
