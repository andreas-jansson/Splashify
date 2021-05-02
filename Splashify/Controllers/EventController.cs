using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class EventController : Controller
    {

        List<EventModel> eventObj = new List<EventModel>();

        public EventController()
        {

        }

        public ActionResult SetEvent(string EventNameField, string EventDateField, string EventGenderField, int Judge1Field, int Judge2Field, int Judge3Field)
        {
            Console.WriteLine("SetEvent triggered!");

            EventModel eventObj = new EventModel();
            eventObj.eventID = EventNameField;
            eventObj.startdate = EventDateField;
            eventObj.gender = EventGenderField;
            SqliteDataAccess.SaveEvent(eventObj);

            return View("~/Views/Home/Application.cshtml");
        }

        public ActionResult GetEvent()
        {

            StringBuilder eventListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>Name</th><th>Date</th><th>Gender</th></tr>");

            eventObj = SqliteDataAccess.LoadEvent();

            foreach (var e in eventObj)
            {
                eventListHtml.Append("<tr><td>");
                eventListHtml.Append(e.eventID);
                eventListHtml.Append("</td><td>");
                eventListHtml.Append(e.startdate);
                eventListHtml.Append("</td><td>");
                eventListHtml.Append(e.gender);
                eventListHtml.Append("</td></tr>");
            }

            eventListHtml.Append("</table>");


            ViewBag.events = eventListHtml;

            return View("~/Views/Home/Application.cshtml");

        }




    }

}




