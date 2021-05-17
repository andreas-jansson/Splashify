using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;

namespace Splashify.Controllers
{
    public class DashboardStatsController : Controller
    {
        public DashboardStatsController()
        {

        }

        public ActionResult GoogleGraph()
        {

            var tupleList = new List<(string, int)>
            {
                ("cow",3),
                ("chickens",6),
                ("airplane",2)
            };





            string datastr = JsonConvert.SerializeObject(tupleList, Formatting.None);

            ViewBag.dataj = new HtmlString(datastr);
            return RedirectToAction("Dashboard", "Home");
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