using System.ComponentModel.DataAnnotations;

namespace F1Teams.Models
{
    /// <summary>
    /// Domain class modeling a Team.
    /// </summary>
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required field!")]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters long!")]
        [MaxLength(25, ErrorMessage = "Name must be at most 25 characters long!")]
        public string Name { get; set; }

        [Range(1, 9999, ErrorMessage = "Year of formation must be a number between 1 and 9999!")]
        public int? YearFormed { get; set; }

        [Range(0, 999, ErrorMessage = "Championships won must be a number between 0 and 999!")]
        public int? ChampionshipsWon { get; set; }

        [Required]
        public bool HasPaidEntryFee { get; set; }
    }
}
