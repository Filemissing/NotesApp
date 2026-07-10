namespace NotesApp.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public bool IsComplete { get; set; }

        public bool IsDraft { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public List<Tag> Tags { get; set; } = new();
    }
}