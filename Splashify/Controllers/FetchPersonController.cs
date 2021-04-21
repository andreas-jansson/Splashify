using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class FetchPersonController : Controller
    {
        List<PersonModel> people = new List<PersonModel>(); 

        public FetchPersonController()
        {

        }

        public ActionResult GetPerson()
        {
            Console.WriteLine("SetPerson triggered!");


            people = SqliteDataAccess.LoadPeople();

            ViewBag.ppl = people;

            return View("~/Views/Home/Scoring.cshtml");
        }

    }
}
