using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreProject.Models;

namespace AspNetCoreProject.Controllers
{


    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var loggedInUserName  = HttpContext.User.Identity.Name; // This is our username we set earlier in the claims. 
                var loggedInUserName2 = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value; //Another way to 
            }
            return View();
        }

        [ServiceFilter(typeof(CommonActionFilter))]
        public IActionResult Setting()
        {
            

            return View();

        }

        
        [HttpPost]
        [ServiceFilter(typeof(CommonActionFilter))]
        public IActionResult Setting(string sho)
        {
            return View();

        }


        [ServiceFilter(typeof(CommonActionFilter))]
        public IActionResult About()
        {
            //if(HttpContext.User.Identity.IsAuthenticated)
            //{
            //    var loggedInUserName = HttpContext.User.Identity.Name; // This is our username we set earlier in the claims. 
            //    var loggedInUserName2 = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value; //Another way to 
            //}
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ServiceFilter(typeof(CommonActionFilter))]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
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
