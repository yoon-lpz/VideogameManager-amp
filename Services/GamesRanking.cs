using System.Xml.Linq;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GamesRanking
    {
        readonly string xmlPath =  Path.GetFullPath(@".\wwwroot\data\games_ranking.xml");
        private List<Game> _topGames;
        XDocument _xmlDoc;

        public void GetRanking(List<Game> games)
        {
            _topGames = games.OrderByDescending(g => g.Score).ToList();

            _xmlDoc = new XDocument(
                new XElement("AppConfig",
                    new XElement("AppTitle", "Videogame Ranking"),
                    new XElement("Games",
                        _topGames.Select(g => new XElement("Game",
                            new XElement("id", g.Id),
                            new XElement("score", g.Score),
                            new XElement("title", g.Title),
                            new XElement("genre", g.Genre),
                            new XElement("year", g.Year)
                        ))
                    )
                )
            );
            _xmlDoc.Save(xmlPath);
        }
    }
}
