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
        List<EventJumpModel> jumpObjList = new List<EventJumpModel>();
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
            eventObj.startdate = createEventObj.startdate;
            eventObj.eventtype = createEventObj.eventtype;

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
        //gets the current/upcoming event for the judge
        public ActionResult GetEvent()
        {
   

            string query = "select j.eventID, j.competitorID, u.fname," +
                "  u.lname,  j.jumpnr, j.jumptype, us.fname as JudgeFirstName ," +
                " us.lname as JudgeLastName, s.score, j.finalscore " +
                "from jump as j " +
                "left join score as s on j.jumpID = s.jumpID " +
                "inner join competitor as c on c.competitorID = j.competitorID " +
                "inner join user as u on u.userID = c.userID " +
                "inner join judge as ju on ju.judgeID = s.judgeID " +
                "inner join user as us on us.userID = ju.userID " +
                "where j.eventID = @eventID " +
                "group by j.jumpID, s.judgeID";


            EventJumpModel eventjump = new EventJumpModel();
            eventjump.userID = (int)HttpContext.Session.GetInt32("UserID");
            Console.WriteLine("userID: " + eventjump.userID);
            StringBuilder jumpListHtml = new StringBuilder("<table id=\"jumpTbl\">" +
                "<tr><th>Event Name</th><th>Competitor ID</th>" +
                "<th>First Name</th><th>Last Name</th><th>Jump nr</th><th>Jump Type</th>" +
                "<th>Judge First Name</th><th>Judge Last Name</th><th>Score</th><th>Final Score</th></tr>");


            string query_original = "select j.jumpID, j.eventID, j.competitorID," +
                " u.fname,  u.lname,  j.jumpnr, s.judgeID, s.score, j.finalscore" +
                " from jump as j inner join score as s on j.jumpID = s.jumpID " +
                "inner join competitor as c on c.competitorID = j.competitorID " +
                "inner join user as u on u.userID = c.userID " +
                "where j.eventID = @eventID group by j.jumpID, s.judgeID";

            string query_upcoming_event = "SELECT e.eventID from event as e " +
                "inner join eventjudge as ej on ej.eventID = e.eventID " +
                "inner join judge as j on j.judgeID = ej.judgeID " +
                "inner join user as u on u.userID = j.userID " +
                "where u.userID=@userID and startdate>=date('now') " +
                "order by startdate " +
                "asc limit 1";

            eventjump=SqliteDataAccess.SingleObject(eventjump, query_upcoming_event);

            if(eventjump == null)
            {
                return RedirectToAction("Scoring", "Home");

            }
            Console.WriteLine("userID: " + eventjump.eventID);

            jumpObjList = SqliteDataAccess.LoadEventJumps(eventjump, query);
            Console.WriteLine("list: " + jumpObjList);

            int i = 1;
            foreach (var jump in jumpObjList)
            {
                Console.WriteLine("Ev: " + jump.eventID);
                jumpListHtml.Append("<tr id="+i+"><td>");
                jumpListHtml.Append(eventjump.eventID);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.competitorID);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.fname);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.lname);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.jumpnr);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.jumptype);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.JudgeFirstName);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.JudgeLastName);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.score);
                jumpListHtml.Append("</td><td>");
                jumpListHtml.Append(jump.finalscore);
                jumpListHtml.Append("</td></tr>");
            }

            jumpListHtml.Append("</table>");


            TempData["eventjumps"] = jumpListHtml.ToString();


            return RedirectToAction("Scoring", "Home");

        }

        public ActionResult UppcomingEvents()
        {

            string query = "select * from event where startdate > date('now')";
            EventModel eventObj = new EventModel();

            eventObjList=SqliteDataAccess.LoadManyObjects(eventObj, query);

            StringBuilder eventListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th style='width:25px'>&nbsp;</th><th>Name</th><th>Date</th><th>Gender</th></tr>");
            int i = 1;
            foreach (var e in eventObjList)
            {
                // onclick='javascript:alert(\"hej\")'
                eventListHtml.Append("<tr id=" + i + "><td width='25px'><img src='../css/images/plus_icon.png' class='icon' onclick='javascript:document.getElementById(\"eventID\").value=\"" + e.eventID + "\"'></td><td>");
                eventListHtml.Append(e.eventID);
                eventListHtml.Append("</td><td>");
                eventListHtml.Append(e.startdate);
                eventListHtml.Append("</td><td>");
                eventListHtml.Append(e.gender);
                eventListHtml.Append("</td></tr>");
            }

            eventListHtml.Append("</table>");

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




