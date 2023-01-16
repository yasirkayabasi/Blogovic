using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogovic.Models.Entity
{
    [Table("Users")]
    public class User
    {
        public User(){}

        public User(string email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
      
        }

        public User(int id, string email)
        {
            Id = id;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage ="Lütfen Email Formatında Giriniz")]
        public string Email { get; set; }
       
    }
}
