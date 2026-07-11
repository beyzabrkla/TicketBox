using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Auth.Commands;
using TicketBox.Domain.Entities;

namespace TicketBox.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(IMediator mediator, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            // MediatR login işlemini  hallediyor
            var result = await _mediator.Send(command);

            if (result)
            {
                // Kullanıcıyı bul
                var user = await _userManager.FindByEmailAsync(command.Email);

                // Kullanıcının rollerini al
                var roles = await _userManager.GetRolesAsync(user);

                // Role göre yönlendir
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "AdminDashboard", new { area = "Admin" });
                }

                // Varsayılan olarak User'a veya User area'sına gönder
                return RedirectToAction("Index", "UserDashboard", new { area = "User" });
            }

            ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
            return View(command);
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterCommand command)
        {
            // 1. FluentValidation kurallarına takılan var mı?
            if (!ModelState.IsValid)
                return View(command);

            try
            {
                // 2. MediatR ile kayıt işlemini başlat
                var result = await _mediator.Send(command);

                if (result)
                {
                    return RedirectToAction("SignIn");
                }

                ModelState.AddModelError(string.Empty, "Kayıt işlemi başarısız oldu.");
                return View(command);
            }
            // Pipeline'dan gelen ValidationException'ı yakalıyoruz
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    // Hataları ModelState'e ekliyoruz ki View'da gözüksün
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(command); // Hatalı verilerle sayfayı tekrar gösteriyoruz
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // sistemin Admin/User area bağlamından çıkıp kök dizine dönmesini sağlıyoruz.
            return RedirectToAction("SignIn", "Auth", new { area = "" });
        }
    }
}