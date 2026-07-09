using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents
{
    public class FooterViewComponent :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
