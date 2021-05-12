using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;
using Microsoft.AspNetCore.Http;

namespace Splashify.Controllers
{
    public class EventController : Controller
    {
        List<EventJumpModel> jumpObj = new List<EventJumpModel>();
        List<EventModel> eventObjList = new List<EventModel>();


        public EventController()
        {

        }

        public ActionResult CreateEvent(CreateEventModel createEventObj)
        {
            Console.WriteLine("SetEvent triggered!");

            EventModel eventObj = new EventModel();
            eventObj.eventID = createEventObj.eventID;
            eventObj.startdate = createEventObj.startdate;
            eventObj.gender = createEventObj.gender;
            Console.WriteLine(eventObj.eventID);
            Console.WriteLine(eventObj.startdate);
            Console.WriteLine(eventObj.gender);

            SqliteDataAccess.SaveEvent(eventObj);


            EventJudgeModel eventJudgeObj1 = new EventJudgeModel();
            EventJudgeModel eventJudgeObj2 = new EventJudgeModel();
            EventJudgeModel eventJudgeObj3 = new EventJudgeModel();

            eventJudgeObj1.eventID = createEventObj.eventID;
            eventJudgeObj1.judgeID = createEventObj.judge1ID;

            eventJudgeObj2.eventID = createEventObj.eventID;
            eventJudgeObj2.judgeID = createEventObj.judge2ID;

            eventJudgeObj3.eventID = createEventObj.eventID;
            eventJudgeObj3.judgeID = createEventObj.judge3ID;

            List<EventJudgeModel> eventJudgeObjList = new List<EventJudgeModel>();

            eventJudgeObjList.Add(eventJudgeObj1);
            eventJudgeObjList.Add(eventJudgeObj2);
            eventJudgeObjList.Add(eventJudgeObj3);

            foreach (var item in eventJudgeObjList){
                Console.WriteLine("item: " + item.eventID + " " + item.judgeID);

            }


                string query = "insert into eventjudge(eventID, judgeID) values(@eventID, @judgeID)";
            SqliteDataAccess.SaveManyObjects(eventJudgeObjList, query);


            return View("~/Views/Home/Managment.cshtml");
        }

        public ActionResult GetEventList()
        {

            StringBuilder eventListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>Name</th><th>Date</th><th>Gender</th></tr>");

            eventObjList = SqliteDataAccess.LoadEvent();

            foreach (var e in eventObjList)
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

        public ActionResult UppcomingEvents()
        {

            string query = "select * from event where startdate > date('now')";
            EventModel eventObj = new EventModel();

            eventObjList=SqliteDataAccess.LoadManyObjects(eventObj, query);

            StringBuilder eventListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>Name</th><th>Date</th><th>Gender</th></tr>");
            int i = 1;
            foreach (var e in eventObjList)
            {
                eventListHtml.Append("<tr id="+i+"><td>");
                eventListHtml.Append(e.eventID);
                eventListHtml.Append("</td><td>");
                eventListHtml.Append(e.startdate);
                eventListHtml.Append("</td><td>");
                eventListHtml.Append(e.gender);
                eventListHtml.Append("</td></tr>");
            }

            eventListHtml.Append("</table>");


            //ViewBag.UpcomingEvents = eventListHtml;
            TempData["UpcomingEvents"] = eventListHtml.ToString();

            return RedirectToAction("Application", "Home");
        }


        //Club applies to participate in an event
        public ActionResult EventApplication(string eventID)
        {
            string query1 = "select * from club where userID = @userID";


            ClubModel club = new ClubModel();
            club.userID = (int)HttpContext.Session.GetInt32("UserID");

            club = SqliteDataAccess.SingleObject(club, query1);

            string query2 = "insert into eventapplication(clubID, eventID) values(@clubID, @eventID)";


            EventApplicationModel application = new EventApplicationModel();
            application.eventID = eventID;
            application.clubID = club.clubID;

            SqliteDataAccess.SaveSingleObject(application, query2);

            return RedirectToAction("Application", "Home");

        }


    }

}




