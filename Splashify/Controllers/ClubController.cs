using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class ClubController : Controller
    {

        List<UserModel> user = new List<UserModel>();
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

        //Adds member to eventcompetitor if club is in eventclub table
        public ActionResult EnrollMember(string eventID, int userID)
        {
            //checks if club is allowed to submit members for said event
            EnrolledUserModel obj = new EnrolledUserModel();
            obj.eventID = eventID;
            obj.userID = (int)HttpContext.Session.GetInt32("UserID");
            string query1 = "select * from eventclub as ec join club as c on ec.clubID = c.clubID where userID = @userID";
            Console.WriteLine("1."+ eventID +" "+userID );
            obj = SqliteDataAccess.SingleObject(obj, query1);

            if (obj == null)
            {
                Console.WriteLine("NULL");
                return View("~/Views/Home/Application.cshtml");

            }
            else
            {
                CompetitorModel competitor = new CompetitorModel();
                competitor.userID = userID;
                string query3 = "select * from competitor where userID = @userID";
                competitor = SqliteDataAccess.SingleObject(competitor, query3);
                competitor.eventID = eventID;

                string query2 = "insert into eventcompetitor(eventID, competitorID) values(@eventID, @competitorID)";
                SqliteDataAccess.SaveSingleObject(competitor, query2);

                for (int i = 1; i < 7; i++)
                {
                    CompetitorModel comp = new CompetitorModel();
                    comp.eventID = eventID;
                    comp.competitorID = competitor.competitorID;
                    comp.jumpnr = i;
                    competitorList.Add(comp);


                }
                string query4 = "insert into jump(eventID, competitorID, jumpnr) values(@eventID, @competitorID, @jumpnr)";
                SqliteDataAccess.SaveManyObjects(competitorList, query4);

            }
            return View("~/Views/Home/Application.cshtml");

        }
    }
}