using System;
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
          


    }
}
