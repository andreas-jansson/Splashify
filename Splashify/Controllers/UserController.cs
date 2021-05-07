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
            newUser.role = user.RoleField;


            UserModel replica =SqliteDataAccess.UserExist(newUser);
            if (replica == null)
            {
                Console.WriteLine("replica: " + replica);
                if (ModelState.IsValid)
                {
                    // Saves User in user table
                    SqliteDataAccess.SavePerson(newUser);

                    // inserts role application 
                    SqliteDataAccess.RoleApplication(newUser);
                    //first time login sets session
                    HttpContext.Session.SetString("UserSession", "default");
                    HttpContext.Session.SetString("UserName", newUser.fname);

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


        public ActionResult GetSingleObject()
        {
            Console.WriteLine("GetObject triggered!");


            // Test Event
            EventModel obj = new EventModel();
            obj.eventID="vm2021";

            obj = SqliteDataAccess.SingleObject(obj, "event", "eventID");

            Console.WriteLine(obj.eventID);
            Console.WriteLine(obj.startdate);
            Console.WriteLine(obj.gender);


            // Test user
            UserModel obj2 = new UserModel();
            obj2.birthdate = "960810-1111";

            obj2 = SqliteDataAccess.SingleObject(obj2, "user", "birthdate");

            Console.WriteLine(obj2.fname);
            Console.WriteLine(obj2.lname);
            Console.WriteLine(obj2.role);

            // Test club
            ClubModel obj3= new ClubModel();
            obj3.clubname = "sthlm finest";

            obj3 = SqliteDataAccess.SingleObject(obj3, "club", "clubname");

            Console.WriteLine(obj3.clubID);
            Console.WriteLine(obj3.clubname);
            Console.WriteLine(obj3.userID);




            return View("~/Views/Home/Scoring.cshtml");
        }

        //Test
        public ActionResult test(LoginModel loginUser)
        {
            Console.WriteLine(loginUser.email);
            Console.WriteLine(loginUser.password);


            return View("~/Views/Home/Managment.cshtml");
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
                    HttpContext.Session.SetString("UserClub", user.club);

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
                return View("~/Views/Home/Dashboard.cshtml");
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
