using CsvHelper;
using System.Globalization;
using System.Text;
using VideoGameManager.Models;
using VideoGameManager.Pages.Games;

namespace VideoGameManager.Services
{
    public class GamesExporter
    {
        private readonly string[] _headers = { "Id", "Title", "Genre", "Year", "Description", "Score" };
        public string csvPath = Path.GetFullPath(@".\wwwroot\data\games.csv");

        public void ExportToCsv(IEnumerable<Game> games)
        {
            var csvBuilder = new StringBuilder();

            csvBuilder.AppendLine(string.Join(",", _headers));

            foreach (var game in games)
            {
                var values = new[]
                {
                    game.Id.ToString(),
                    game.Title.Contains(",") ? $"\"{game.Title}\"" : game.Title,
                    game.Genre,
                    game.Year.ToString(),
                    game.Description,
                    game.Score.ToString(CultureInfo.InvariantCulture)
                };
                csvBuilder.AppendLine(string.Join(",", values));
            }
            var bytes =  Encoding.UTF8.GetBytes(csvBuilder.ToString());
            File.WriteAllBytes(csvPath, bytes);
        }

        public List<Game> ImportFromCsv()
        {
            var games = new List<Game>();
            if (!File.Exists(csvPath)) return games;

            var lines = File.ReadAllLines(csvPath);

            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    var game = lines[i].Split(',');

                    if (game.Length == 6) {
                        games.Add(new Game
                        {
                            Id = int.Parse(game[0]),
                            Title = game[1].Trim('"'),
                            Genre = game[2],
                            Year = int.Parse(game[3]),
                            Description = game[4],
                            Score = double.Parse(game[5], CultureInfo.InvariantCulture)
                        });
                    }
                }
            }
            return games;
        }
    }
}
