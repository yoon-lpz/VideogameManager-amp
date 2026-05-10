using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly GameStoreContext _context;

        public int EmptyStars, FilledStars;

        [BindProperty]
        public Game Game { get; set; }

        public DeleteModel(GameStoreContext context) => _context = context;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _context.Games
                .Include(g => g.Developer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Game == null)
            {
                return NotFound();
            }

            FilledStars = (int)(Game.Score / 2);
            EmptyStars = 5 - FilledStars;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var gameToDelete = await _context.Games.FindAsync(Game.Id);

            if (gameToDelete != null)
            {
                try
                {
                    _context.Games.Remove(gameToDelete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return RedirectToPage("./Delete", new { id = Game.Id, saveChangesError = true });
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
