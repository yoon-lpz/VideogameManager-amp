using Microsoft.AspNetCore.Components.Web;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameService
    {
        private static int _nextId = 1;
        private string _line;
        private List<Game> _games = new();
        public string logPath = Path.GetFullPath(@".\wwwroot\data\activity_log.txt");

        public GameService()
        {
            _nextId = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
        }

        /// <summary>
        /// Returns the complete list of games stored in the system.
        /// </summary>
        /// <returns>A list of all Game objects.</returns>
        public List<Game> GetAll() => _games;

        /// <summary>
        /// Looks for a specific game using its ID.
        /// </summary>
        /// <param name="id">The ID of the game to find.</param>
        /// <returns>The game that matches the ID, or null if it is not found.</returns>
        public Game GetById(int id) => _games.FirstOrDefault(g => g.Id == id);

        /// <summary>
        /// Adds a new game to the list.
        /// </summary>
        /// <param name="game">The game object to be added.</param>
        public void Add(Game game)
        {
            game.Id = _nextId++;
            _games.Add(game);
            LogActivity("ADD", game);
        }

        /// <summary>
        /// Updates an existing game in the list if its ID matches.
        /// </summary>
        /// <param name="game">The game object with the updated information.</param>
        public void Update(Game game)
        {
            var index = _games.FindIndex(g => g.Id == game.Id);
            if (index >= 0) _games[index] = game;
            LogActivity("UPDATE", game);
        }

        /// <summary>
        /// Removes a game from the list based on its ID.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        public void Delete(int id)
        {
            var game = GetById(id);
            if (game != null) _games.Remove(game);
            LogActivity("DELETE", game);
        }

        /// <summary>
        /// Logs a specific action to a text file with a timestamp.
        /// </summary>
        /// <param name="action">The action being recorded.</param>
        /// <param name="game">The <see cref="Game"/> object whose title will be logged.</param>
        private void LogActivity(string action, Game game)
        {
            _line = $"[{DateTime.Now:yy/MM/dd HH:mm:ss}] [{action}] {game.Title}{Environment.NewLine}";
            File.AppendAllText(logPath, _line);
        }
    }
}
