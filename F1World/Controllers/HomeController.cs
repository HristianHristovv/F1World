using F1World.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
            // ���������� ViewData
            ViewData["Title"] = "F1 World";

            // 1?? ������� F1 Standings �� API
            var standingsUrl = "https://ergast.com/api/f1/current/driverStandings.json";
            var drivers = new List<dynamic>(); // ������ ����, ��� API �� ����� ���������

            try
            {
                var response = await _httpClient.GetStringAsync(standingsUrl);
                var json = JObject.Parse(response);
                drivers = json["MRData"]["StandingsTable"]["StandingsLists"][0]["DriverStandings"].ToObject<List<dynamic>>();
            }
            catch (Exception ex)
            {
                _logger.LogError("������ ��� ������� �� Standings: " + ex.Message);
            }

            ViewData["Drivers"] = drivers;

            // 2?? ������� F1 ������ (�������� RSS feed)
            var newsUrl = "https://www.formula1.com/en/latest/all.xml"; // ������ � ������� RSS JSON API
            string newsHtml = "<p>������ ��� ��������� �� ��������.</p>";

            try
            {
                var newsResponse = await _httpClient.GetStringAsync(newsUrl);
                newsHtml = $"<pre>{newsResponse}</pre>"; // ��� ������ �� ��������� XML ��� HTML
            }
            catch (Exception ex)
            {
                _logger.LogError("������ ��� ��������� �� ������: " + ex.Message);
            }

            ViewData["News"] = newsHtml;

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
