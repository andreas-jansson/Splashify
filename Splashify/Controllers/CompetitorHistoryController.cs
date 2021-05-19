using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;

namespace Splashify.Controllers
{
    public class CompetitorHistoryController : Controller
    {
        List<SearchModel> SearchObj = new List<SearchModel>();

        public ActionResult CompetitorHistory()
        {
            Console.WriteLine("CompetitorHistory triggered!");

            return View();

        }
    }
}