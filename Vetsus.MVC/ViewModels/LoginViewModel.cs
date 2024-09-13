using System.ComponentModel.DataAnnotations;

namespace Vetsus.MVC.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Password { get; set; }
    }
}
