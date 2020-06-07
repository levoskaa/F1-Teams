using F1Teams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace F1Teams.DAL
{
    /// <summary>
    /// Interface declaring the necessary methods of a Team repository.
    /// </summary>
    public interface ITeamRepository
    {
        /// <summary>
        /// Adds a Team to the repository.
        /// </summary>
        /// <param name="team">The Team to be addded.</param>
        Task Insert(Team team);

        /// <summary>
        /// Gets all Teams from the repository.
        /// </summary>
        /// <returns>Collection of Team instances.</returns>
        IReadOnlyCollection<Team> List();
		
        /// <summary>
        /// Finds a Team by id.
        /// </summary>
        /// <param name="id">Id to be searched for.</param>
        /// <returns>Team with the given id or null if not found.</returns>
        Task<Team> FindById(int id);

        /// <summary>
        /// Updates a Team instance.
        /// </summary>
        /// <param name="team">Team to be updated.</param>
        Task Update(Team team);

        /// <summary>
        /// Delete a Team from the repository.
        /// </summary>
        /// <param name="id">Id of the Team to be deleted.</param>
        /// <returns>Bool that represents a succes flag.</returns>
        Task<bool> Delete(int id);
    }
}
