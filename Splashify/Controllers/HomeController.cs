using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

            List<SelectListItem> events2 = new List<SelectListItem>()
            {
                new SelectListItem {Text = "Shyju", Value="1"},
                new SelectListItem {Text = "Sean", Value="2"},
            };
            ViewBag.OptionEventList= events2;

            return View();
        }
  

        public IActionResult Dashboard()
        {
            return View();
        }


        public IActionResult Managment()
        {
            return View();
        }


        public IActionResult Application()
        {
            Console.WriteLine("App");
            if (HttpContext.Session.GetString("UserSession") == "club") {
                var eventlist = new ClubController().ApprovedEventList((int)HttpContext.Session.GetInt32("UserID"));
              /*  TempData["SaveEventList"] = eventlist;
                TempData.Keep("SaveEventList");*/
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
