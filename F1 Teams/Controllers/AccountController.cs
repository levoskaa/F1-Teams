using F1Teams.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace F1Teams.Controllers
{
    /// <summary>
    /// Controller for managing user authentication.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Provides the APIs for user sign in.
        /// </summary>
        private readonly SignInManager<IdentityUser> signInManager;

        /// <summary>
        /// Data received in a sign in request.
        /// </summary>
        [BindProperty]
        public SignInViewModel signInData { get; set; }

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// Processes the sign in requests.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInPost()
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(signInData.UserName, signInData.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username & password combination.");
                }
            }
            return View("SignIn", signInData);
        }

        /// <summary>
        /// Processes the sign out requests.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
