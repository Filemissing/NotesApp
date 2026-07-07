namespace NotesApp.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
