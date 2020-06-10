using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace F1Teams.DAL.EfDbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<DbTeam> Teams { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbTeam>()
                .ToTable("teams")
                .HasKey(team => team.Id);
            builder.Entity<DbTeam>()
                .Property(team => team.Name)
                .HasMaxLength(25)
                .IsRequired(required: true);
            builder.Entity<DbTeam>()
                .Property(team => team.YearFormed);
            builder.Entity<DbTeam>()
                .Property(team => team.ChampionshipsWon);
            builder.Entity<DbTeam>()
                .Property(team => team.HasPaidEntryFee)
                .IsRequired(required: true);

            // Seeding data
            string roleId = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            var passwordHasher = new PasswordHasher<IdentityUser>();
            var user = new IdentityUser
            {
                Id = userId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "f1test2018");
            // Seed admin role
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Id = roleId, Name = "admin", NormalizedName = "ADMIN" });
            // Seed admin user
            builder.Entity<IdentityUser>()
                .HasData(user);
            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string> { RoleId = roleId, UserId = userId });
        }
    }
}
