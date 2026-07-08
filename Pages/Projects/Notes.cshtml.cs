using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Pages.Projects
{
    [IgnoreAntiforgeryToken]
    public class NotesModel : PageModel
    {
        private readonly AppDbContext _context;

        public Project Project { get; set; }

        public Note SelectedNote { get; set; }


        public NotesModel(AppDbContext context)
        {
            _context = context;
        }


        public async Task OnGetAsync(int id, int? noteId)
        {
            Project = await _context.Projects
                .Include(p => p.Notes)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (Project == null)
            {
                return;
            }


            if (noteId != null)
            {
                SelectedNote = Project.Notes
                    .FirstOrDefault(n => n.Id == noteId);
            }
            else
            {
                SelectedNote = Project.Notes.FirstOrDefault();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync(int id)
        {
            var note = new Note
            {
                Title = "Untitled Note",
                Content = "",
                ProjectId = id
            };


            _context.Notes.Add(note);

            await _context.SaveChangesAsync();


            return RedirectToPage("./Notes", new
            {
                id = id,
                noteId = note.Id
            });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, int noteId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == noteId);


            if (note == null)
            {
                return NotFound();
            }


            _context.Notes.Remove(note);

            await _context.SaveChangesAsync();


            return RedirectToPage("./Notes", new
            {
                id = id
            });
        }

        public async Task<IActionResult> OnPostSaveAsync([FromBody] SaveRequest request)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == request.NoteId);

            if (note == null)
            {
                return NotFound();
            }

            note.Title = request.Title;
            note.Content = request.Content;

            await _context.SaveChangesAsync();

            return new JsonResult(new
            {
                success = true
            });
        }

        public IActionResult OnPostPreviewAsync([FromBody] PreviewRequest request)
        {
            var html = NotesApp.Helpers.MarkdownHelper.ToHtml(request.Content);

            return Content(html, "text/html");
        }
    }

    public class SaveRequest
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }

    public class PreviewRequest
    {
        public string Content { get; set; }
    }
}