using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameService _gameService;

        [BindProperty]
        public Game Game { get; set; }

        public CreateModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            Game = new Game();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _gameService.Add(Game);
            return RedirectToPage("./Index");
        }
    }
}
