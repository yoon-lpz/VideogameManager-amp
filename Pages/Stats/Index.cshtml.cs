using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;

namespace VideoGameManager.Pages.Stats
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Threshold { get; set; } = 0;

        private readonly GameStoreContext _context;
        public IndexModel(GameStoreContext context) => _context = context;

        public IList<Game> FilteredGames { get; set; }
        public IList<Game> Top5Games { get; set; }
        public IList<Game> AllGames { get; set; }
        public IList<Game> FilteredByNameGames { get; set; }
        public List<dynamic> GamesByDecade { get; set; }
        public List<dynamic> AvgPerDev { get; set; }



        [BindProperty(SupportsGet = true)]
        public string SelectedGenre { get; set; }
        public SelectList Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TitleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MinYear { get; set; }

        public IList<Developer> ProductiveDevs { get; set; }

        public async Task OnGetAsync()
        {
            var genres = await _context.Games
                .Select(g => g.Genre)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();

            Genres = new SelectList(genres);

            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SelectedGenre))
            {
                query = query.Where(g => g.Genre == SelectedGenre);
            }

            FilteredGames = await query.OrderByDescending(g => g.Score).ToListAsync();

            AllGames = await _context.Games
                .ToListAsync();

            Top5Games = await _context.Games
                .Include(g => g.Developer)
                .OrderByDescending(g => g.Score)
                .Take(5)
                .ToListAsync();

            GamesByDecade = await _context.Games
                .GroupBy(g => (g.Year / 10) * 10)
                .Select(grp => (dynamic)new { Decade = grp.Key, Count = grp.Count() })
                .ToListAsync();

            var devData = await _context.Developers
                .Include(d => d.Games)
                .Where(d => d.Games.Any())
                .Select(d => new {
                    d.Name,
                    GameCount = d.Games.Count,
                    AvgScore = d.Games.Average(g => g.Score)
                })
                .OrderByDescending(x => x.AvgScore)
                .ToListAsync();

            AvgPerDev = devData.Select(d => (dynamic)d).ToList();

            var query2 = _context.Games.Include(g => g.Developer).AsQueryable();

            if (!string.IsNullOrWhiteSpace(TitleFilter)) query2 = query2.Where(g => g.Title.Contains(TitleFilter));

            if (!string.IsNullOrWhiteSpace(SelectedGenre)) query2 = query2.Where(g => g.Genre == SelectedGenre);

            if (MinYear.HasValue) query2 = query2.Where(g => g.Year >= MinYear.Value);

            FilteredByNameGames = await query.OrderBy(g => g.Title).ToListAsync();
            ProductiveDevs = await _context.Developers
                .Include(d => d.Games)
                .Where(d => d.Games.Count > Threshold)
                .OrderByDescending(d => d.Games.Count)
                .ToListAsync();
        }
    }
}