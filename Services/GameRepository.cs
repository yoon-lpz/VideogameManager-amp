using System.Text.Json;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameRepository
    {
        public string repositoryPath = Path.GetFullPath(@".\wwwroot\data\games.json");
        private string _json;

        /// <summary>
        /// Loads the list of games from the JSON file.
        /// </summary>
        /// <returns>A list of <see cref="Game"/> objects; returns an empty list if the file does not exist or cannot be deserialized.</returns>
        public List<Game> LoadAll()
        {
            if (!File.Exists(repositoryPath)) return new List<Game>();
            _json = File.ReadAllText(repositoryPath);

            try
            {
                return JsonSerializer.Deserialize<List<Game>>(_json) ?? new List<Game>();
            }
            catch (Exception e)
            {
                return new List<Game>();
            }
        }

        /// <summary>
        /// Serializes the list of games into JSON format and saves it to the file.
        /// </summary>
        /// <param name="games">The list of <see cref="Game"/> objects to be saved.</param>
        public void SaveAll(List<Game> games)
        {
            _json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(repositoryPath, _json);
        }
    }
}
