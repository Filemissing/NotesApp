using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;

namespace NotesApp.ViewComponents
{
    public class ProjectSidebarViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProjectSidebarViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projects = await _context.Projects
                .OrderBy(p => p.Name)
                .ToListAsync();

            return View(projects);
        }
    }
}