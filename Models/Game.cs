using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VideoGameManager.Models
{
    public class Game
    {

        private const string TitleErrorMsg = "You must input a title";
        private const string GenreErrorMsg = "You must input a genre";
        private const string YearErrorMsg = "Not a valid year";
        private const string ScoreErrorMsg = "Score must be between 0 and 10";

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = TitleErrorMsg)]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = GenreErrorMsg)]
        public string Genre { get; set; } = string.Empty;

        [Range(1970, 2030, ErrorMessage = YearErrorMsg)]
        public int Year { get; set; }

        [Range(0, 10, ErrorMessage = ScoreErrorMsg)]
        public double Score { get; set; }

        public string Description { get; set; } = string.Empty;

        // Clau forània cap a Developer
        public int DeveloperId { get; set; }

        // Propietat de navegació
        public Developer? Developer { get; set; }
    }

}
