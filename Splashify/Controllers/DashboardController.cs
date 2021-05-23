using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;
using System.Collections;

namespace Splashify.Controllers
{
    public class DashboardStatsController : Controller
    {
        public DashboardStatsController()
        {

        }

        public ArrayList GoogleGraph()
        {

            string competitor_query = "SELECT COUNT(*) FROM User WHERE role = 'competitor'";
            string club_query = "SELECT COUNT(*) FROM User WHERE role = 'club'";
            string judge_query = "SELECT COUNT(*) FROM User WHERE role = 'judge'";

            int obj = new int();

            int numcompetitors = SqliteDataAccess.SingleObject<int>(obj, competitor_query);
            int numclubs = SqliteDataAccess.SingleObject<int>(obj, club_query);
            int numjudges = SqliteDataAccess.SingleObject<int>(obj, judge_query);




            ArrayList header = new ArrayList {"title", "title2" };
            ArrayList data1 = new ArrayList { "Club Admins", numclubs };
            ArrayList data2 = new ArrayList { "Judges", numjudges };
            ArrayList data3 = new ArrayList { "Competitors", numcompetitors };
            ArrayList data = new ArrayList { header, data1, data2, data3 };

            return data;
        }


        public ActionResult DashboardInfo()
        {




            return RedirectToAction("Dashboard", "Home");
        }


        public string GetUsersStats()
        {
            string competitor_query = "SELECT COUNT(*) FROM User WHERE role = 'competitor'";
            string club_query = "SELECT COUNT(*) FROM User WHERE role = 'club'";
            string judge_query = "SELECT COUNT(*) FROM User WHERE role = 'judge'";

            int obj = new int();

            int numcompetitors = SqliteDataAccess.SingleObject<int>(obj, competitor_query);
            int numclubs = SqliteDataAccess.SingleObject<int>(obj, club_query);
            int numjudges = SqliteDataAccess.SingleObject<int>(obj, judge_query);

            StringBuilder UserRoleStatsHTML = new StringBuilder();

            UserRoleStatsHTML.Append("<p>Competitors: ");
            UserRoleStatsHTML.Append(numcompetitors);
            UserRoleStatsHTML.Append("</p>");
            UserRoleStatsHTML.Append("<p>Clubs: ");
            UserRoleStatsHTML.Append(numclubs);
            UserRoleStatsHTML.Append("</p>");
            UserRoleStatsHTML.Append("<p>Judges: ");
            UserRoleStatsHTML.Append(numjudges);
            UserRoleStatsHTML.Append("</p>");

            return UserRoleStatsHTML.ToString();
        }

        public int GetNumEvents()
        {
            return SqliteDataAccess.LoadEvent().Count();
        }

        public string GetNextEvent()
        {
            string datestr = SqliteDataAccess.GetNextEventDate().Substring(0, 10);
            return datestr;
        }
    }
}