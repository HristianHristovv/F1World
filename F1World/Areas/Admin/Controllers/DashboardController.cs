using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F1World.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Само администратори
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
