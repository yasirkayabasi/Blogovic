using System.ComponentModel.DataAnnotations;

namespace Blogovic.ViewModels.Auth.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Boş Bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
