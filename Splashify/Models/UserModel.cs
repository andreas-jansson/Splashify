using System;
namespace Splashify.Models
{
    public class UserModel
    {
        public int userID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string birthdate { get; set; }
        public string gender { get; set; }
        public string club { get; set; }
        public string role { get; set; }
    }
}
