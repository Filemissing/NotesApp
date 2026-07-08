using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.Projects
{
    public class TasksModel : PageModel
    {
        private readonly AppDbContext _context;

        public Project Project { get; set; }

        public TasksModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _context.Projects
                .Include(p => p.Tasks
                    .OrderBy(t => t.IsComplete)
                    .ThenBy(t => t.Id))
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Project == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostToggleTaskAsync(int id, int taskId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            task.IsComplete = !task.IsComplete;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostRenameProjectAsync(int projectId, string name)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            project.Name = name.Trim();

            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = projectId });
        }

        public async Task<IActionResult> OnPostDeleteTaskAsync(int id, int taskId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id });
        }

        public async Task<IActionResult> OnPostCreateTaskAsync(int projectId, string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return RedirectToPage(new { id = projectId });
            }

            var task = new ProjectTask
            {
                Title = title,
                ProjectId = projectId,
                IsComplete = false
            };

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = projectId });
        }
    }
}