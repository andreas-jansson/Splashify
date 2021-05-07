using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;

namespace Splashify.Controllers
{
    public class ApplicationHandlerController : Controller
    {

        List<RoleApplicationModel> applications = new List<RoleApplicationModel>();
        List<EventApplicationModel> events = new List<EventApplicationModel>();


        public ApplicationHandlerController()
        {
        }

        public ActionResult RoleApplication(int userIDField, string button)
        {
            Console.WriteLine("LoadRoleApplication triggered!");
            RoleApplicationModel applicant = new RoleApplicationModel();
            applicant.userID = userIDField;
            Console.WriteLine("ID: " + applicant.userID);
            Console.WriteLine("role: " + applicant.role);


            if (button == "refresh") {

                StringBuilder applicationListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>User ID</th><th>First Name</th><th>Last Name</th><th>Role</th></tr>");
                applications = SqliteDataAccess.LoadRoleApplication();

                foreach (var app in applications)
                {
                    applicationListHtml.Append("<tr><td>");
                    applicationListHtml.Append(app.userID);
                    applicationListHtml.Append("</td><td>");
                    applicationListHtml.Append(app.fname);
                    applicationListHtml.Append("</td><td>");
                    applicationListHtml.Append(app.lname);
                    applicationListHtml.Append("</td><td>");
                    applicationListHtml.Append(app.role);
                    applicationListHtml.Append("</td></tr>");
                }

                applicationListHtml.Append("</table>");

                Console.WriteLine(applicationListHtml);

                ViewBag.RoleApps = applicationListHtml;
            }
            else if (button == "accept" && userIDField != 0)
            {
                //Finds the requested role
                applicant.role = SqliteDataAccess.SingleObjectString(applicant, "roleapplication", "userID", "role");
                SqliteDataAccess.ApproveRole(applicant);

            }
            else if (button == "deny" && userIDField != 0)
            {
                SqliteDataAccess.DenyRole(applicant);
            }
            else
            {
                Console.WriteLine("Shit went wrong!");
            }

            return View("~/Views/Home/Managment.cshtml");

        }


        public ActionResult EventApplication(int ID, string button){

            EventApplicationModel eventApplication = new EventApplicationModel();
            eventApplication.ID = ID;
            Console.WriteLine("id: " + eventApplication.ID);
            Console.WriteLine("btn: " + button);

            if (button == "refresh")
            {
                StringBuilder applicationListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>ID</th><th>Event ID</th><th>Club ID</th><th>Club Name</th><th>Gender</th><th>Date</th></tr>");
                events = SqliteDataAccess.LoadEventApplication();

                int i = 1;

                foreach (var app in events)
                {
                    applicationListHtml.Append("<tr id="+i+"><td>");
                    applicationListHtml.Append(app.ID);
                    applicationListHtml.Append("</td><td>");
                    applicationListHtml.Append(app.eventID);
                    applicationListHtml.Append("</td><td>");
                    applicationListHtml.Append(app.clubID);
                    applicationListHtml.Append("</td><td>");
                    applicationListHtml.Append(app.clubname);
                    applicationListHtml.Append("</td><td>");
                    applicationListHtml.Append(app.gender);
                    applicationListHtml.Append("</td><td>");
                    applicationListHtml.Append(app.startdate);
                    applicationListHtml.Append("</td></tr>");
                }

                applicationListHtml.Append("</table>");

                Console.WriteLine(applicationListHtml);

                ViewBag.EventApps = applicationListHtml;
            }
            else if (button == "accept" && ID != 0)
            {
                eventApplication.eventID = SqliteDataAccess.SingleObjectString(eventApplication, "eventapplication", "ID", "eventID");
                eventApplication.clubID = Int32.Parse(SqliteDataAccess.SingleObjectString(eventApplication, "eventapplication", "ID", "clubID"));

                Console.WriteLine("event id: " + eventApplication.eventID);
                Console.WriteLine("club id: " + eventApplication.clubID);
                SqliteDataAccess.ApproveEvent(eventApplication);
            }
            else if (button == "deny" && ID != 0)
            {
                SqliteDataAccess.DenyEvent(eventApplication);

            }
            else
            {
                Console.WriteLine("Shit went wrong!");

            }


            return View("~/Views/Home/Managment.cshtml");

        }

    }
}
