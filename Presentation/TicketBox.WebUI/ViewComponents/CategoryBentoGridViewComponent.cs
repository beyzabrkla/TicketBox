using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents
{
    public class CategoryBentoGridViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
