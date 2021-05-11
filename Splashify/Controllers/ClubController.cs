using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Splashify.Models;

namespace Splashify.Controllers
{
    public class ClubController : Controller
    {
        private List<UserModel> user = new List<UserModel>();
        private List<ClubModel> clubs = new List<ClubModel>(); 
        List<EnrolledUserModel> enrolled = new List<EnrolledUserModel>();
        List<CompetitorModel> competitorList = new List<CompetitorModel>();


        public ClubController()
        {
        }

        public ActionResult ClubMembers()
        {
            string query = "select u.userID, u.club, u.fname, u.lname, " +
                "u.birthdate, u.gender from user as u join club as c " +
                "on u.club = c.clubID where c.userID = @userID";
            UserModel userObj = new UserModel();
            userObj.userID = (int)HttpContext.Session.GetInt32("UserID");
            user = SqliteDataAccess.LoadManyObjects(userObj, query);

            StringBuilder userListHtml = new StringBuilder("<table id=\"pplTbl\">" +
                "<tr><th>User ID</th><th>Club</th><th>First Name</th>" +
                "<th>Last Name</th><th>Birthdate</th><th>Gender</th></tr>");

            foreach (var person in user)
            {
                userListHtml.Append("<tr><td>");
                userListHtml.Append(person.userID);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.club);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.fname);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.lname);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.birthdate);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.gender);
                userListHtml.Append("</td></tr>");
            }


            userListHtml.Append("</table>");


            ViewBag.ClubMembers = userListHtml;

            return View("~/Views/Home/Application.cshtml");
        }



        //Displays clubmembers who are enrolled in upcoming events
        //  Något galet här! fel userid och kön bland annat
        public ActionResult EnrolledMembers()
        {
            string query = "select * from user as u inner join competitor as c " +
                "on u.userID = c.userID " +
                "inner join club as cl on cl.clubID = u.club " +
                "inner join eventcompetitor as ec on ec.competitorID = c.competitorID " +
                "inner join event as e on e.eventID = ec.eventID " +
                "where cl.userID = @userID and startdate >= date('now')";

            EnrolledUserModel userObj = new EnrolledUserModel();
            userObj.userID = (int)HttpContext.Session.GetInt32("UserID");
            enrolled = SqliteDataAccess.LoadManyObjects(userObj, query);

            StringBuilder userListHtml = new StringBuilder("<table id=\"pplTbl\">" +
                "<tr><th>Event</th><th>User ID</th><th>Club</th><th>First Name</th>" +
                "<th>Last Name</th><th>Birthdate</th><th>Gender</th><th>Date</th></tr>");

            foreach (var person in enrolled)
            {
                Console.WriteLine("event: " + person.eventID);
                userListHtml.Append("<tr><td>");
                userListHtml.Append(person.eventID);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.userID);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.clubname);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.fname);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.lname);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.birthdate);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.gender);
                userListHtml.Append("</td><td>");
                userListHtml.Append(person.startdate);
                userListHtml.Append("</td></tr>");
            }


            userListHtml.Append("</table>");


            ViewBag.EnrolledMembers = userListHtml;

            return View("~/Views/Home/Application.cshtml");

        }


        //Dynamic select options for enrollmember
        public ActionResult EnrollMember()
        {
            
            Console.WriteLine("get triggerd");
            var events = new CompetitorModel();

            events.eventList = new List<SelectListItem> {
                new SelectListItem {Text = "test1", Value="1"},
                new SelectListItem { Text = "test2", Value="2"}
            };


           List <SelectListItem> events2 = new List<SelectListItem>()
            {
                new SelectListItem {Text = "Shyju", Value="1"},
                new SelectListItem {Text = "Sean", Value="2"},
            };

            //ViewBag.OptionEventList= events2;


            List<SelectListItem> eventID = new List<SelectListItem>();
            eventID.Add(new SelectListItem() { Text = "Shyju", Value = "1" });
            eventID.Add(new SelectListItem() { Text = "test2", Value = "2" });
            eventID.Add(new SelectListItem() { Text = "test3", Value = "3" });


            eventID.Insert(eventID.Count, new SelectListItem { Text = "Others", Value = eventID.Count.ToString() });

            CompetitorModel comp = new CompetitorModel();
            ViewBag.OptionEventList = eventID;

            // return (IEnumerable<SelectListItem>)events;

            return View(events);

        }


        //Adds member to eventcompetitor if club is in eventclub table
        [HttpPost]
        public ActionResult EnrollMember(CompetitorModel comp)
        {
            Console.WriteLine("post triggerd");


            var events = new CompetitorModel();

            events.eventList = new List<SelectListItem> {
                new SelectListItem {Text = "test1", Value="1"},
                new SelectListItem { Text = "test2", Value="2"}
            };

            //ViewBag.OptionEventList = events2;

            //checks if club is allowed to submit members for said event
            EnrolledUserModel obj = new EnrolledUserModel();
            obj.eventID = comp.eventID;
            obj.userID = (int)HttpContext.Session.GetInt32("UserID");
            string query1 = "select * from eventclub as ec join club as c on ec.clubID = c.clubID where userID = @userID";
            Console.WriteLine("1."+ comp.eventID +" "+comp.userID );
            obj = SqliteDataAccess.SingleObject(obj, query1);

            if (obj == null)
            {
                Console.WriteLine("NULL");
                return View("~/Views/Home/Application.cshtml");

            }
            else
            {
                CompetitorModel competitor = new CompetitorModel();
                competitor.userID = comp.userID;
                string query3 = "select * from competitor where userID = @userID";
                competitor = SqliteDataAccess.SingleObject(competitor, query3);
                competitor.eventID = comp.eventID;

                string query2 = "insert into eventcompetitor(eventID, competitorID) values(@eventID, @competitorID)";
                SqliteDataAccess.SaveSingleObject(competitor, query2);

                for (int i = 1; i < 7; i++)
                {
                    CompetitorModel comp2 = new CompetitorModel();
                    comp2.eventID = comp.eventID;
                    comp2.competitorID = competitor.competitorID;
                    comp2.jumpnr = i;
                    competitorList.Add(comp2);


                }
                string query4 = "insert into jump(eventID, competitorID, jumpnr) values(@eventID, @competitorID, @jumpnr)";
                SqliteDataAccess.SaveManyObjects(competitorList, query4);

            }
            return View(events);
        }


        public ActionResult ClubApplication(int clubID)
        {
            ClubModel club = new ClubModel();
            club.clubID = clubID;
            club.userID = (int)HttpContext.Session.GetInt32("UserID");
            string query = "insert into clubapplication(clubID, userID) values(@clubID, @userID)";

            SqliteDataAccess.SaveSingleObject(club, query);

            return View("~/Views/Home/Application.cshtml");

        }

        public ActionResult Clubs()
        {
            string query = "select clubID, userID, clubname from club order by clubname";
            //+ "where userID = @userID";

            ClubModel clubObj = new ClubModel();
            clubObj.userID = (int)HttpContext.Session.GetInt32("UserID");
            clubs = SqliteDataAccess.LoadManyObjects(clubObj, query);


            StringBuilder clubListHtml = new StringBuilder("<table id=\"clubTbl\">" +
              "<tr><th>Club</th><th>Club Id</th><th>User Id</th></tr>");

            foreach (var club in clubs)
            {
                clubListHtml.Append("<tr><td>");
                clubListHtml.Append(club.clubname);
                clubListHtml.Append("</td><td>");
                clubListHtml.Append(club.clubID);
                clubListHtml.Append("</td><td>");
                clubListHtml.Append(club.userID);
                clubListHtml.Append("</td></tr>");
            }

            clubListHtml.Append("</table>");


            ViewBag.Clubs = clubListHtml;

            return View("~/Views/Home/Application.cshtml");
        }

    }
}