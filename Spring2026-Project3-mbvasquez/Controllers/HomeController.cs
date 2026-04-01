using Microsoft.AspNetCore.Mvc;
using Spring2026_Project3_mbvasquez.Models;
using System.Diagnostics;

namespace Spring2026_Project3_mbvasquez.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Movies()
        {
            return View();
        }
        public IActionResult Actors()
        {
            return View();
        }
        //public async Task<IActionResult> Tweets()
        //{
        //    var tweets = await AIGen.TwitterApiSim();
        //    return View(tweets);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
