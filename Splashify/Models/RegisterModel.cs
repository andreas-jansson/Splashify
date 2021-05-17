using System;
using System.ComponentModel.DataAnnotations;

namespace Splashify.Models
{

    public class RegisterModel
    {

        public string FnameField { get; set; }

        [Required(ErrorMessage = "* Enter a last name")]
        public string LnameField { get; set; }

        [Required(ErrorMessage = "* Enter a valid e-mail")]
        public string BirthField { get; set; }

        [Required(ErrorMessage = "* Enter a valid e-mail")]
        [EmailAddress]
        public string EmailField { get; set; }

        [Required(ErrorMessage = "* Enter a password")]
        [DataType(DataType.Password)]
        public string PasswordField { get; set; }

        [Required(ErrorMessage = "* Renter a password")]
        [Compare(nameof(PasswordField), ErrorMessage = "* Passwords don't match.")]
        [DataType(DataType.Password)]
        public string PasswordConfField { get; set; }

        [Required(ErrorMessage = "* Dont be a snowflake")]
        public string GenderField { get; set; }

        [Required(ErrorMessage = "* Select a role")]
        public string RoleField { get; set; }

    }
}
