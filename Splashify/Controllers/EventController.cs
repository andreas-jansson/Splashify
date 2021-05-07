using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class EventController : Controller
    {
        List<EventJumpModel> jumpObj = new List<EventJumpModel>();
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

        public ActionResult GetEventList()
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

            return View("~/Views/Home/Scoring.cshtml");

        }

        public ActionResult GetEvent(string eventID)
        {

            EventJumpModel eventjump = new EventJumpModel();
            eventjump.eventID = eventID;
            Console.WriteLine("event: "+eventjump.eventID);

            StringBuilder jumpListHtml = new StringBuilder("<table id=\"jumpTbl\">" +
                "<tr><th>Jump ID</th><th>Event ID</th><th>Competitor ID</th>" +
                "<th>First Name</th><th>Last Name</th><th>Jump nr</th>" +
                "<th>Judge ID</th><th>Score</th><th>Final Score</th></tr>");


            string query = "select j.jumpID, j.eventID, j.competitorID," +
                " u.fname,  u.lname,  j.jumpnr, s.judgeID, s.score, j.finalscore" +
                " from jump as j inner join score as s on j.jumpID = s.jumpID " +
                "inner join competitor as c on c.competitorID = j.competitorID " +
                "inner join user as u on u.userID = c.userID " +
                "where j.eventID = @eventID group by j.jumpID, s.judgeID;";

            Console.WriteLine(query);
            jumpObj = SqliteDataAccess.LoadEventJumps(eventjump, query);

            int i = 1;
            foreach (var jump in jumpObj)
            {
                jumpListHtml.Append("<tr id="+i+"><td>");
                jumpListHtml.Append(jump.jumpID);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.eventID);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.competitorID);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.fname);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.lname);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.jumpnr);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.judgeID);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.score);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.finalscore);
                jumpListHtml.Append("</td></tr>");
            }

            jumpListHtml.Append("</table>");


            ViewBag.eventjumps = jumpListHtml;

            return View("~/Views/Home/Scoring.cshtml");

        }




    }

}




