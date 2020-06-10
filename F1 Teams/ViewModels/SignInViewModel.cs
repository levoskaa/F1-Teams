using System.ComponentModel.DataAnnotations;

namespace F1Teams.ViewModels
{
    /// <summary>
    /// ViewModel for the sign in page, contains the data necessary for the sign in operation.
    /// </summary>
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Username is a required field!")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is a required field!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
