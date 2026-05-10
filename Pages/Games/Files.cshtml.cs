using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class FilesModel : PageModel
    {
        private readonly GameService _gameService;
        private readonly GameRepository _repository;
        private readonly GamesExporter _exporter;
        private readonly GamesRanking _ranking;
        private List<Game> _games;
        public List<string> Log { get; set; } = new();

        public FilesModel(GameService gameService, GameRepository repository, GamesExporter gamesExporter, GamesRanking gamesRanking)
        {
            _gameService = gameService;
            _repository = repository;
            _exporter = gamesExporter;
            _ranking = gamesRanking;
        }

        public void OnGet()
        {
            if (System.IO.File.Exists(_gameService.logPath))
            {
                Log = System.IO.File.ReadAllLines(_gameService.logPath).ToList();
            }
        }

        public IActionResult OnPostExportJson()
        {
            _repository.SaveAll(_gameService.GetAll());
            return RedirectToPage();
        }

        public IActionResult OnPostImportJson()
        {
            _games = _repository.LoadAll();
            _gameService.GetAll().Clear();
            _gameService.GetAll().AddRange(_games);
            return RedirectToPage();
        }

        public IActionResult OnPostExportCsv()
        {
            var _games = _gameService.GetAll();

            if (_games == null || !_games.Any())
            {
                return RedirectToPage();
            }

            _exporter.ExportToCsv(_games);
            return RedirectToPage();
        }

        public IActionResult OnPostImportCsv()
        {
            if (!System.IO.File.Exists(_exporter.csvPath))
            {
                return RedirectToPage();
            }
            var _games = _exporter.ImportFromCsv();

            if (_games != null && _games.Any())
            {
                _gameService.GetAll().Clear();
                _gameService.GetAll().AddRange(_games);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostGenerateXml()
        {
            _ranking.GetRanking(_gameService.GetAll());
            return RedirectToPage();
        }
    }
}
