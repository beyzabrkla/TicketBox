using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
