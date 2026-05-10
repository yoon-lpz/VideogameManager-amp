using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly GameService _gameService;

        public int EmptyStars, FilledStars;

        [BindProperty]
        public Game Game { get; set; }

        public DeleteModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet(int id)
        {
            Game = _gameService.GetById(id);
            FilledStars = (int)Game.Score / 2;
            EmptyStars = 5 - FilledStars;
        }
        public IActionResult OnPost()
        {
            _gameService.Delete(Game.Id);
            return RedirectToPage("./Index");
        }
    }
}
