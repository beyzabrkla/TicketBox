using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
