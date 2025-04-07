using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace F1World.Controllers
{
    public class FantasyController : Controller
    {
        public IActionResult Index()
        {
            var drivers = new List<string> {
                "Max Verstappen",
                "Charles Leclerc",
                "Lewis Hamilton",
                "Lando Norris"
            };

            var teams = new List<string> {
                "Red Bull",
                "Ferrari",
                "Mercedes",
                "McLaren"
            };

            ViewBag.Drivers = drivers;
            ViewBag.Teams = teams;

            return View();
        }
    }
}
