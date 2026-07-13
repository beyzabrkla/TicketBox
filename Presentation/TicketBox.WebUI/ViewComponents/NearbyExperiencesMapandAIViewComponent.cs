using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Interfaces;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents
{
    public class NearbyExperiencesMapandAIViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
