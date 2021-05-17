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




    }
}
