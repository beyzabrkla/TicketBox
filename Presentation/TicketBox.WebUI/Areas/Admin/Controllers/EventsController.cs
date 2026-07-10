using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
