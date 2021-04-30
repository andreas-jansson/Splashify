using System;
using System.ComponentModel.DataAnnotations;

namespace Splashify.Models
{
  
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string passsword { get; set; }
    }
}
