using F1World.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace F1World.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "F1 World";

            // 🏎️ Взимаме класирането на пилотите от API
            var standingsUrl = "https://ergast.com/api/f1/current/driverStandings.json";
            var drivers = new List<DriverStandingsModel>();

            try
            {
                var response = await _httpClient.GetStringAsync(standingsUrl);
                var json = JObject.Parse(response);

                drivers = json["MRData"]["StandingsTable"]["StandingsLists"][0]["DriverStandings"]
                    .ToObject<List<DriverStandingsModel>>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Грешка при зареждане на Standings: {ex.Message}");
            }

            ViewData["Drivers"] = drivers;

            // 📰 Взимаме новини от XML RSS
            var newsList = new List<NewsItem>();
            try
            {
                var rssUrl = "https://www.formula1.com/en/latest/all.xml";
                var stream = await _httpClient.GetStreamAsync(rssUrl);
                var doc = XDocument.Load(stream);

                newsList = doc.Descendants("item")
                    .Select(item => new NewsItem
                    {
                        Title = item.Element("title")?.Value,
                        Link = item.Element("link")?.Value,
                        Description = item.Element("description")?.Value
                    })
                    .Take(10)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Грешка при зареждане на новините: " + ex.Message);
            }

            ViewData["News"] = newsList;

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
