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

            //checks for input null from user
            if(Search.SearchField == "null" || Search.SearchField == "Null" || Search.SearchField == "NULL")
            {
                return RedirectToAction("Dashboard", "Home");
            }



            SearchModel s = new SearchModel();
            s.SearchField = Search.SearchField;
            s.Value = Search.Value;

            StringBuilder SearchListHtml;

            if (s.Value == 1)
            {
                SearchListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>First Name</th><th>Last Name</th><th>Event</th><th>Jumpnr</th><th>Finalscore</th></tr>");

                SearchObj = SqliteDataAccess.LoadSearch(s);
                if (SearchObj == null) 
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                foreach (var e in SearchObj)
                {
                    SearchListHtml.Append("<tr><td>");
                    SearchListHtml.Append(e.Fname);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.Lname);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.EventID);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.Jumpnr);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.Finalscore);
                    SearchListHtml.Append("</td></tr>");
                }
            }
            else if (s.Value == 2)
            {
                SearchListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>First Name</th><th>Last Name</th><th>Event</th><th>Date</th></tr>");

                SearchObj = SqliteDataAccess.LoadSearch(s);
                if (SearchObj == null)
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                foreach (var e in SearchObj)
                {
                    SearchListHtml.Append("<tr><td>");
                    SearchListHtml.Append(e.Fname);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.Lname);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.EventID);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.startdate);
                    SearchListHtml.Append("</td></tr>");
                }
            }
            else if (s.Value == 3)
            {
                SearchListHtml = new StringBuilder("<table id=\"pplTbl\"><tr><th>Event</th><th>date</th><th>Competitor</th><th>Jumpnr</th><th>Finalscore</th></tr>");

                SearchObj = SqliteDataAccess.LoadSearch(s);
                if (SearchObj == null)
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                foreach (var e in SearchObj)
                {
                    SearchListHtml.Append("<tr><td>");
                    SearchListHtml.Append(e.EventID);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.startdate);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.CompetitorID);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.Jumpnr);
                    SearchListHtml.Append("</td><td>");
                    SearchListHtml.Append(e.Finalscore);
                    SearchListHtml.Append("</td></tr>");
                }
            }
            else
            {
                Console.WriteLine("Error");
                return RedirectToAction("Dashboard", "Home");
            }


            SearchListHtml.Append("</table>");

            TempData["search"] = SearchListHtml.ToString();
            return RedirectToAction("Dashboard", "Home");

        }

    }

}