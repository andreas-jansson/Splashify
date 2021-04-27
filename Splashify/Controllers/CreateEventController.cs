using System;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class CreateEventController : Controller
    {

        public CreateEventController()
        {

        }

        public ActionResult SetEvent(string EventNameField, string EventDateField, string EventGenderField, int Judge1Field, int Judge2Field, int Judge3Field)
        {
            Console.WriteLine("SetEvent triggered!");

            EventModel e = new EventModel();
            e.name = EventNameField;
            e.date = EventDateField;
            e.gender = EventGenderField;
            e.judge1ID = Judge1Field;
            e.judge1ID = Judge2Field;
            e.judge1ID = Judge3Field;


            SqliteDataAccess.SaveEvent(e);

            return View("~/Views/Home/Managment.cshtml");
        }
    }

}




