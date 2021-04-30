using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Splashify.Models;
using Newtonsoft.Json;

namespace Splashify.Controllers
{
    public class LoginController : Controller
    {
         UserModel user = new UserModel();


        public LoginController()
        {

        }

        public ActionResult Authorize(string email, string password)
        {
            UserModel u = new UserModel();
            u.email = email;
            u.password = password;

            UserModel user = SqliteDataAccess.AuthorizeUser(u);

            if (user != null)
            {

                if (user.password == password)
                {
                    Console.WriteLine("Authenticated!");
                    HttpContext.Session.SetString("UserSession", user.role);
                    HttpContext.Session.SetString("UserName", user.fname);
                    return View("~/Views/Home/Dashboard.cshtml");

                }
                else
                {
                    Console.WriteLine("Access Denied!");
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                Console.WriteLine("Access Denied!");
                return View("~/Views/Home/Login.cshtml");
            }

        }

        public ActionResult Logout()
        {

            HttpContext.Session.Remove("UserSession");
            HttpContext.Session.Remove("UserName");
            return View("~/Views/Home/Dashboard.cshtml");
        }





    }
}
