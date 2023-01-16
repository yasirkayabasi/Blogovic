using System.ComponentModel.DataAnnotations;

namespace Blogovic.ViewModels.Auth.Register
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email Boş Bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
