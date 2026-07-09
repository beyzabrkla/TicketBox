using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents
{
    public class HeroSectionViewComponent :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
