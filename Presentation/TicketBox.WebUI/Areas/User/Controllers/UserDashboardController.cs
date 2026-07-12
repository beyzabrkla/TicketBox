using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Application.Features.Tickets.Queries;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class UserDashboardController : Controller
    {
        private readonly IMediator _mediator;

        public UserDashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> MyTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // MediatR üzerinden sorguyu gönder
            var query = new GetTicketQuery { UserId = userId };
            var result = await _mediator.Send(query);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTicket(int id)
        {
            // Claims üzerinden aktif kullanıcıyı alıyoruz
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                // Komutu gönderiyoruz (Handler içerisinde userId kontrolü yapıldığı için güvenli)
                var command = new RemoveTicketCommand { TicketId = id, UserId = userId };
                await _mediator.Send(command);

                // İşlem başarılıysa kullanıcıya bilgi ver
                TempData["Success"] = "Biletiniz başarıyla iptal edildi ve kontenjan güncellendi.";
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya mesaj göster
                TempData["Error"] = "İptal işlemi gerçekleşemedi: " + ex.Message;
            }

            return RedirectToAction("MyTickets");
        }
    }
}