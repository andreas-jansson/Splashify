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
    }
}