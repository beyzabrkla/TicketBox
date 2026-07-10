using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
        
        public IActionResult SignUp()
        {
            return View();
        }

    }
}
