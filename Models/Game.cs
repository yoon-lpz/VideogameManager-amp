using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VideoGameManager.Models
{
    public class Game
    {
        private const string TitleError = "You must input a title.",
            GenreError = "You must input a genre.",
            YearError = "Not a valid year.",
            ScoreError = "The score must be between 0 and 10 (included).";

        private int _id, _year;
        private double _score;
        private string _title, _genre, _description = string.Empty;

        public int Id { get => _id; set => _id = value; }

        [Required(ErrorMessage = TitleError)]
        [StringLength(100)]
        public string Title { get => _title; set => _title = value; }

        [Required(ErrorMessage = GenreError)]
        public string Genre { get => _genre; set => _genre = value;  }

        [Range(1970, 2030, ErrorMessage = YearError)]
        public int Year { get => _year; set => _year = value; }

        [Range(0, 10, ErrorMessage = ScoreError)]
        public double Score { get => _score; set => _score = value; }

        public string? Description { get => _description; set => _description = value; } 
    }
}
