using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents
{
    public class NearbyExperiencesMapandAIViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
