using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly GameStoreContext _context;

        [BindProperty]
        public Game Game { get; set; }

        public SelectList DeveloperList { get; set; }

        public EditModel(GameStoreContext context) => _context = context;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _context.Games.FindAsync(id);

            if (Game == null) return NotFound();

            await LoadDevelopersAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDevelopersAsync();
                return Page();
            }

            _context.Attach(Game).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Game.Id });
        }

        private async Task LoadDevelopersAsync()
        {
            var developers = await _context.Developers.ToListAsync();
            DeveloperList = new SelectList(developers, "Id", "Name");
        }

        private bool GameExists(int id) => _context.Games.Any(g => g.Id == id);
    }
}
