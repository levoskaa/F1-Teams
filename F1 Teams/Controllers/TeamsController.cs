using F1Teams.DAL;
using F1Teams.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace F1Teams.Controllers
{
    /// <summary>
    /// Controller for the Teams page.
    /// </summary>
    public class TeamsController : Controller
    {
        /// <summary>
        /// Team that comes from Http requests.
        /// </summary>
        [BindProperty]
        public Team Team { get; set; }
        /// <summary>
        /// Repository for accessing the Team instances.
        /// </summary>
        private readonly ITeamRepository teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public IActionResult Index()
        {
            var teams = teamRepository.List();
            return View(teams);
        }

        /// <summary>
        /// Manages the creation / modification of Teams.
        /// </summary>
        /// <param name="id">Id of the Team to be updated. Null means a new instance will be created.</param>
        public async Task<IActionResult> Upsert(int? id)
        {
            Team = new Team();
            if (id == null)
            {
                return View(Team);
            }
            Team = await teamRepository.FindById((int)id);
            if (Team == null)
            {
                return NotFound();
            }
            return View(Team);
        }

        /// <summary>
        /// Adds a new Team to the repository.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert()
        {
            if (ModelState.IsValid)
            {
                await teamRepository.Insert(Team);
                return RedirectToAction("Index");
            }
            else
            {
                return View(Team);
            }
        }

        // https://stackoverflow.com/a/10907343/13236465
        // The PUT method is disabled (405 - Method Not Allowed) in IIS Express 10,
        // to enable it I would have had to modify the config file. That's why POST is used here.
        /// <summary>
        /// Updates a Team.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update()
        {
            if (ModelState.IsValid)
            {
                await teamRepository.Update(Team);
                return RedirectToAction("Index");
            }
            else
            {
                return View(Team);
            }
        }

        #region API calls
        /// <summary>
        /// Deletes a Team from the repository.
        /// </summary>
        /// <param name="id">Id of the Team to be deleted.</param>
        /// <returns>Json containing a success flag and a message.</returns>
        [HttpDelete]
        [Route("/api/teams")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await teamRepository.Delete(id);
            if (!result)
            {
                return Json(new { success = false, message = "Error during delete operation" });
            }
            else
            {
                return Json(new { success = true, message = "Delete operation successful" });
            }
        }
        #endregion
    }
}
