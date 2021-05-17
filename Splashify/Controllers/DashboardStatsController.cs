using System;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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

        public ActionResult Dashboard()
        {
            var data = new List<statsModel>() {
                new statsModel(){ Category = 18, Frequence=4},
                new statsModel(){ Category = 20, Frequence=6},
                new statsModel(){ Category = 22, Frequence=5},
                new statsModel(){ Category = 24, Frequence=2}
            };

            string str = JsonConvert.SerializeObject(data, Formatting.None);

            ViewBag.dataj = new HtmlString(data.ToString());

            /*
             tillhörande chartjs
            ScoreModel p = new ScoreModel();

            var list = SqliteDataAccess.LoadFinalScore();
            List<int> reparations = new List<int>();
            var score = list.Select(x => x.Score).Distinct();
            Console.WriteLine(score);
            foreach(var item in score)
            {
                reparations.Add(list.Count(x => x.Score == item));
            }

            var rep = reparations;
            ViewBag.SCORE = score;
            ViewBag.REP = reparations.ToList();

            Console.WriteLine(list);*/
            return View();
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
            return datestr.Substring(6, 4) + "-" + datestr.Substring(3, 2) + "-" + datestr.Substring(0, 2);
        }



    }
}
