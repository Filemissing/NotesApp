namespace NotesApp.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
