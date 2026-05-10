using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class DetailsModel : PageModel
    {
        public int EmptyStars, FilledStars;
        private readonly GameService _gameService;
        public Game Game { get; set; }

        public DetailsModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet(int id)
        {
            Game = _gameService.GetById(id);
            FilledStars = (int) Game.Score / 2;
            EmptyStars = 5 - FilledStars;
        }
    }
}