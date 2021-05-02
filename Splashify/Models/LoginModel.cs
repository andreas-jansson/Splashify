using System;
using System.ComponentModel.DataAnnotations;

namespace Splashify.Models
{
  
    public class LoginModel
    {
        [Required(ErrorMessage = "* Enter a valid E-mail")]
        [EmailAddress(ErrorMessage = "* Enter a valid E-mail")]
        public string email { get; set; }
        [Required(ErrorMessage = "* Enter a password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
