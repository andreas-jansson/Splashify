using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{ 
    public class UserController : Controller
    {

        List<UserModel> user = new List<UserModel>();


        public UserController()
        {

        }
        /*
        public ActionResult RegisterUser(string FnameField, string LnameField,
            string EmailField, string PasswordField, string PasswordconfField,
            string GenderField, string RoleRequestField)
        {
            Console.WriteLine("SetPerson triggered!");
            Console.WriteLine("role: " + RoleRequestField);
            UserModel user = new UserModel();
            user.fname = FnameField;
            user.lname = LnameField;
            user.email = EmailField;
            user.password = PasswordField;
            user.gender = GenderField;

            if (PasswordField == PasswordconfField)
            {
                SqliteDataAccess.SavePerson(user);
                return View("~/Views/Home/Dashboard.cshtml");

            }
            else
            {
                return View("~/Views/Home/Dashboard.cshtml");

            }
        }
        */

        /* Återstår att skicka rollansökan till annan databas*/
        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {


            UserModel newUser = new UserModel();
            newUser.fname = user.FnameField;
            newUser.lname = user.LnameField;
            newUser.email = user.EmailField;
            newUser.password = user.PasswordField;
            newUser.birthdate = user.BirthField;
            newUser.gender = user.GenderField;


            UserModel replica=SqliteDataAccess.UserExist(newUser);
            if (replica == null)
            {
                Console.WriteLine("replica: " + replica);
                if (ModelState.IsValid)
                {

                    SqliteDataAccess.SavePerson(newUser);
                    return View("~/Views/Home/Dashboard.cshtml");
                }
                else
                {
                    return View("~/Views/Home/Register.cshtml");
                }
            }
            else if(replica.fname == "birthdate duplicate")
            {
                Console.WriteLine("replica: " + replica.fname);
                return View("~/Views/Home/Register.cshtml");

            }
            else if (replica.fname == "email duplicate")
            {
                Console.WriteLine("replica: " + replica.fname);
                return View("~/Views/Home/Register.cshtml");

            }
            else 
            {
                Console.WriteLine("replica: " + replica.fname);
                return View("~/Views/Home/Register.cshtml");

            }

        }

     

            public ActionResult GetUser()
        {
            Console.WriteLine("SetPerson triggered!");

            StringBuilder userListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>First Name</th><th>Last Name</th></tr>");

            user = SqliteDataAccess.LoadPeople();

            foreach (var person in user)
            {
                userListHtml.Append("<tr><td>");
                userListHtml.Append(person.fname);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.lname);
                userListHtml.Append("</td></tr>");
            }

            userListHtml.Append("</table>");

            Console.WriteLine(userListHtml);

            ViewBag.ppl = userListHtml;

            return View("~/Views/Home/Scoring.cshtml");
        }



        public ActionResult Authorize(LoginModel loginUser)
        {
            UserModel u = new UserModel();
            u.email = (string)loginUser.email;
            u.password = (string)loginUser.password;

            UserModel user = SqliteDataAccess.AuthorizeUser(u);

            if (ModelState.IsValid && user != null)
            {

                if (user.password == u.password)
                {
                    Console.WriteLine("Authenticated!");
                    HttpContext.Session.SetString("UserSession", user.role);
                    HttpContext.Session.SetString("UserName", user.fname);
                    return View("~/Views/Home/Dashboard.cshtml");

                }
                else
                {
                    Console.WriteLine("Access Denied!");
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                Console.WriteLine("Access Denied!");
                return View("~/Views/Home/Login.cshtml");
            }

        }

        public ActionResult Logout()
        {

            HttpContext.Session.Remove("UserSession");
            HttpContext.Session.Remove("UserName");
            return View("~/Views/Home/Dashboard.cshtml");
        }
    }
}
