using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly GameService _gameService;

        [BindProperty]
        public Game Game { get; set; }

        public EditModel(GameService gameService) {
            _gameService = gameService;
        }

        public void OnGet(int id)
        {
            Game = _gameService.GetById(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _gameService.Update(Game);
            return RedirectToPage("./Details", new { id = Game.Id });
        }
    }
}
