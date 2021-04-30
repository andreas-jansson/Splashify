using System;
using System.Collections.Generic;
using System.Text;
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

        public ActionResult SetUser(string FnameField, string LnameField)
        {
            Console.WriteLine("SetPerson triggered!");

            UserModel user = new UserModel();
            user.fname = FnameField;
            user.lname = LnameField;


            SqliteDataAccess.SavePerson(user);
            return View("~/Views/Home/Scoring.cshtml");
            /* "~/Views/Home/Scoring.cshtml" */
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
    }
}
