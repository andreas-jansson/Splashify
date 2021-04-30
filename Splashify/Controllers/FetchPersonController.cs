using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{


    public class FetchPersonController : Controller
    {
        List<UserModel> people = new List<UserModel>(); 

        public FetchPersonController()
        {

        }

        public ActionResult GetPerson()
        {
            Console.WriteLine("SetPerson triggered!");

            StringBuilder peopleListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>First Name</th><th>Last Name</th></tr>");

            people = SqliteDataAccess.LoadPeople();

            foreach (var person in people)
            {
                peopleListHtml.Append("<tr><td>");
                peopleListHtml.Append(person.fname);
                peopleListHtml.Append("</td><td>");
                peopleListHtml.Append(person.lname);
                peopleListHtml.Append("</td></tr>");
            }

            peopleListHtml.Append("</table>");

            Console.WriteLine(peopleListHtml);

            ViewBag.ppl = peopleListHtml;

            return View("~/Views/Home/Scoring.cshtml");
        }

    }
}
