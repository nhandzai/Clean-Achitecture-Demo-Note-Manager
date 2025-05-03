namespace NoteApp.Entities
{
    public class Note
    {
        public Guid Id { get;  set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get;  set; }

        private Note()
        {
            Title = string.Empty;
            Content = string.Empty;
        }

        public Note(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty.", nameof(content));

            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }
    }
}