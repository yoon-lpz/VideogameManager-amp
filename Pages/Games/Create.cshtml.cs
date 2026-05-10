using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameStoreContext _context;

        [BindProperty]
        public Game Game { get; set; }

        public SelectList DeveloperList { get; set; }

        public CreateModel(GameStoreContext context) => _context = context;

        public async Task<IActionResult> OnGetAsync()
        {
            var developers = await _context.Developers.ToListAsync();
            DeveloperList = new SelectList(developers, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var developers = await _context.Developers.ToListAsync();
                DeveloperList = new SelectList(developers, "Id", "Name");
                return Page();
            }

            _context.Games.Add(Game);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
