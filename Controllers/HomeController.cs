using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using randomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace randomPasscode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count",1);
            }
            int? count = HttpContext.Session.GetInt32("Count");
            string random = "";
            string number = "0123456789";
            string letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rand = new Random();
            while(random.Length < 14){
                if(rand.Next(1,3)%2 == 0){
                    random+=number[rand.Next(0,10)];
                }
                else{
                    random+=letter[rand.Next(0,26)];
                }
            }
            ViewBag.Count = count;
            ViewBag.Random = random;
            return View();
        }

        [HttpGet("Generate")]
        public IActionResult Generate()
        {
            int? count = HttpContext.Session.GetInt32("Count");
            count ++;
            HttpContext.Session.SetInt32("Count",(int)count);
            return RedirectToAction("Index");
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
