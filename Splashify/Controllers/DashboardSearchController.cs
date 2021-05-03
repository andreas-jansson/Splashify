using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class DashboradSearchController : Controller
    {

        List<SearchModel> SearchObj = new List<SearchModel>();

        public DashboradSearchController()
        {

        }

        /*
        public ActionResult SetSearch(int value, string SearchField, string EventNameField, string EventDateField, string EventGenderField)
        {

            Console.WriteLine("SetSearch triggered!");

            SearchModel s = new SearchModel();
            s.SearchField = SearchField;
            s.value = value;

            SearchModel searchObj = new SearchModel();

            if (value == 1) 
            {
                searchObj.name = EventNameField;
                searchObj.startdate = EventDateField;
                searchObj.gender = EventGenderField;
                SqliteDataAccess.SaveSearch(searchObj);

                return View("~/Views/Home/Dashboard.cshtml");
            }
            else if (value == 2) 
            {
                searchObj.name = EventNameField;
                searchObj.startdate = EventDateField;
                searchObj.gender = EventGenderField;
                SqliteDataAccess.SaveSearch(searchObj);

                return View("~/Views/Home/Dashboard.cshtml");
            }
            else if (value == 3) 
            {
                searchObj.name = EventNameField;
                searchObj.startdate = EventDateField;
                searchObj.gender = EventGenderField;
                SqliteDataAccess.SaveSearch(searchObj);

                return View("~/Views/Home/Dashboard.cshtml");
            }
            else 
            {
                return View("~/Views/Home/Dashboard.cshtml");
            }
        }
        */
        [HttpPost]
        public ActionResult GetSearch(SearchModel Search)
        {

            Console.WriteLine("GetSearch triggered!");

            SearchModel s = new SearchModel();
            s.SearchField = Search.SearchField;
            s.value = Search.value;

            //SearchModel searchObj = new SearchModel();

            if (Search.value == 1)
            {
                Console.WriteLine("value = 1");
                //return View("~/Views/Home/Dashboard.cshtml");
            }
            else if (Search.value == 2)
            {
                Console.WriteLine("value = 2");
                //return View("~/Views/Home/Dashboard.cshtml");
            }
            else if (Search.value == 3)
            {
                Console.WriteLine("value = 3");
                //return View("~/Views/Home/Dashboard.cshtml");
            }
            else
            {
                Console.WriteLine("value = 0");
                Console.WriteLine(Search.value);
                Console.WriteLine(Search.SearchField);
                Console.WriteLine(s.value);
                Console.WriteLine(s.SearchField);
                //return View("~/Views/Home/Dashboard.cshtml");
            }

            StringBuilder SearchListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>Name</th><th>Date</th><th>Gender</th></tr>");

            SearchObj = SqliteDataAccess.CompetitorSearch(s);

            foreach (var e in SearchObj)
            {
                SearchListHtml.Append("<tr><td>");
                SearchListHtml.Append(e.name);
                SearchListHtml.Append("</td><td>");
                SearchListHtml.Append(e.startdate);
                SearchListHtml.Append("</td><td>");
                SearchListHtml.Append(e.gender);
                SearchListHtml.Append("</td></tr>");
            }

            SearchListHtml.Append("</table>");


            ViewBag.search = SearchListHtml;

            return View("~/Views/Home/Dashboard.cshtml");

        }

    }

}