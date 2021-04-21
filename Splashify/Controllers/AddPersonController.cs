using System;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{ 
    public class AddPersonController : Controller
    { 

        public AddPersonController()
        {

        }

        public ActionResult SetPerson(string FnameField, string LnameField)
        {
            Console.WriteLine("SetPerson triggered!");

            PersonModel p = new PersonModel();
            p.Fname = FnameField;
            p.Lname = LnameField;


            SqliteDataAccess.SavePerson(p);
            return View("~/Views/Home/Scoring.cshtml");
            /* "~/Views/Home/Scoring.cshtml" */
        }

    }
}
