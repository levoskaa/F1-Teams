using System.ComponentModel.DataAnnotations;

namespace F1_Teams.DAL.EfDbContext
{
    /// <summary>
    /// Persistent class of the Team domain class.
    /// </summary>
    public class DbTeam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(25)]
        public string Name { get; set; }

        [Range(1, 9999)]
        public int? YearFormed { get; set; }

        [Range(0, 999)]
        public int? ChampionshipsWon { get; set; }

        [Required]
        public bool HasPaidEntryFee { get; set; }
    }
}
