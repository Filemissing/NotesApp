using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public List<Project> Projects { get; set; } = new();

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Projects = _context.Projects
                .Include(p => p.Tasks)
                .ToList();
        }
    }
}