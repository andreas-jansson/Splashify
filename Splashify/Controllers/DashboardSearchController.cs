using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class DashboardSearchController : Controller
    {

        List<SearchModel> SearchObj = new List<SearchModel>();

        public DashboardSearchController()
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
            s.Value = Search.Value;


            StringBuilder SearchListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>EventID</th><th>CompetitorID</th><th>JumpID</th><th>Jumpnr</th><th>Finalscore</th></tr>");

            SearchObj = SqliteDataAccess.LoadSearch(s);

            foreach (var e in SearchObj)
            {
                SearchListHtml.Append("<tr><td>");
                SearchListHtml.Append(e.EventID);
                SearchListHtml.Append("</td><td>");
                SearchListHtml.Append(e.CompetitorID);
                SearchListHtml.Append("</td><td>");
                SearchListHtml.Append(e.JumpID);
                SearchListHtml.Append("</td><td>");
                SearchListHtml.Append(e.Jumpnr);
                SearchListHtml.Append("</td><td>");
                SearchListHtml.Append(e.Finalscore);
                SearchListHtml.Append("</td></tr>");
            }

            SearchListHtml.Append("</table>");


            ViewBag.search = SearchListHtml;

            return View("~/Views/Home/Dashboard.cshtml");

        }

    }

}