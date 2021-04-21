using System;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class ScoringController : Controller
    {

        public ScoringController()
        {

        }

        public ActionResult SetScore(int ContestantIDField, int EventIDField, int JumpNrField, int ScoreField)
        {

            Console.WriteLine(ContestantIDField);
            Console.WriteLine(EventIDField);
            Console.WriteLine(JumpNrField);
            Console.WriteLine(ScoreField);
 
            /*   fungerar att skicka till db
            PersonModel p = new PersonModel();
            p.Fname = ContestantIDField.ToString();
            p.Lname = EventIDField.ToString();

            SqliteDataAccess.SavePerson(p);*/

            return View("~/Views/Home/Scoring.cshtml");
            /* "~/Views/Home/Scoring.cshtml" */

        }

    }
}
