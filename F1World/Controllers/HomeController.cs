using F1World.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            // 📰 Взимаме новини (тук може да използваш истински RSS JSON API)
            var newsUrl = "https://www.formula1.com/en/latest/all.xml"; // Замени с JSON източник
            string newsHtml = "<p>Грешка при зареждане на новините.</p>";

            try
            {
                var newsResponse = await _httpClient.GetStringAsync(newsUrl);
                newsHtml = ConvertUrlsToLinks(newsResponse); // Преобразуваме URL-ите в кликаеми линкове
            }
            catch (Exception ex)
            {
                _logger.LogError("Грешка при зареждане на новини: " + ex.Message);
            }

            ViewData["News"] = newsHtml;

            return View();

        }
        private string ConvertUrlsToLinks(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string pattern = @"(http[s]?:\/\/[^\s]+)";
            string replacement = "<a href=\"$1\" target=\"_blank\">$1</a>";

            return Regex.Replace(text, pattern, replacement);
        }
    }
}
