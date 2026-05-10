using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameService _gameService;
        public List<Game> Games { get; set; } = new();

        public IndexModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            Games = _gameService.GetAll();
        }
    }
}
