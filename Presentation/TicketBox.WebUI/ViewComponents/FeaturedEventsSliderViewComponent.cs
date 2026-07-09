using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents
{
    public class FeaturedEventsSliderViewComponent :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
