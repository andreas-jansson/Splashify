using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Splashify.Models;



namespace Splashify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scoring()
        {
            return View();
        }


        public IActionResult Privacy()
        {

            return View();
        }
  

        public IActionResult Dashboard()
        {


            ArrayList header = new ArrayList {"title", "title2" };
            ArrayList data1 = new ArrayList { "gotta", 2};
            ArrayList data2 = new ArrayList { "go", 3 };
            ArrayList data3 = new ArrayList { "really", 5 };
            ArrayList data4 = new ArrayList { "fast", 8 };
            ArrayList data = new ArrayList { header, data1, data2, data3, data4 };


            string datastr = JsonConvert.SerializeObject(data, Formatting.None);

            ViewBag.dataj = new HtmlString(datastr);

            return View();
        }


        public IActionResult Managment()
        {
            return View();
        }


        public IActionResult Application()
        {
            if (HttpContext.Session.GetString("UserSession") == "club") {
                var eventlist = new ClubController().ApprovedEventList((int)HttpContext.Session.GetInt32("UserID"));
                ViewBag.OptionEventList = eventlist;
            }
            return View();
     
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
