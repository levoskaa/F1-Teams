using F1Teams.DAL.EfDbContext;
using F1Teams.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Teams.DAL
{
    /// <summary>
    /// Team repsitory implementing the ITeamRepository interface.
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext db;

        public TeamRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Insert(Team team)
        {
            var dbTeam = ToDbTeam(team);
            await db.Teams.AddAsync(dbTeam);
            await db.SaveChangesAsync();
        }

        public IReadOnlyCollection<Team> List()
        {
            return db.Teams.ToList()
                .ConvertAll(dbTeam => ToTeam(dbTeam))
                .ToList();
        }

        public async Task Update(Team team)
        {
            var dbTeam = await db.Teams.FindAsync(team.Id);
            if (dbTeam == null)
            {
                return;
            }
            var newTeam = ToDbTeam(team);
            db.Entry(dbTeam).CurrentValues.SetValues(newTeam);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var dbTeam = await db.Teams.FindAsync(id);
            if (dbTeam == null)
            {
                return;
            }
            db.Teams.Remove(dbTeam);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Converts a persistent instance to a domain instance.
        /// </summary>
        /// <param name="dbTeam">Persistent instance to be converted.</param>
        /// <returns>Domain instance after conversion.</returns>
        private Team ToTeam(DbTeam dbTeam)
        {
            return new Team
            {
                Id = dbTeam.Id,
                Name = dbTeam.Name,
                YearFormed = dbTeam.YearFormed,
                ChampionshipsWon = dbTeam.ChampionshipsWon,
                HasPaidEntryFee = dbTeam.HasPaidEntryFee
            };
        }

        /// <summary>
        /// Converts a domain instance to a persistent instance.
        /// </summary>
        /// <param name="team">Domain instance to be converted.</param>
        /// <returns>Persistent instance after conversion.</returns>
        private DbTeam ToDbTeam(Team team)
        {
            return new DbTeam
            {
                Id = team.Id,
                Name = team.Name,
                YearFormed = team.YearFormed,
                ChampionshipsWon = team.ChampionshipsWon,
                HasPaidEntryFee = team.HasPaidEntryFee
            };
        }
    }
}
