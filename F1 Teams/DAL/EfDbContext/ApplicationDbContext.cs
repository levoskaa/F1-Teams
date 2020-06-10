using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace F1Teams.DAL.EfDbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<DbTeam> Teams { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbTeam>()
                .ToTable("teams")
                .HasKey(team => team.Id);
            modelBuilder.Entity<DbTeam>()
                .Property(team => team.Name)
                .HasMaxLength(25)
                .IsRequired(required: true);
            modelBuilder.Entity<DbTeam>()
                .Property(team => team.YearFormed);
            modelBuilder.Entity<DbTeam>()
                .Property(team => team.ChampionshipsWon);
            modelBuilder.Entity<DbTeam>()
                .Property(team => team.HasPaidEntryFee)
                .IsRequired(required: true);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "admin", NormalizedName = "Admin".ToUpper() });
        }
    }
}
