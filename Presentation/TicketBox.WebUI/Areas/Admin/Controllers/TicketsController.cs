using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Features.Tickets.Commands;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TicketsController : Controller
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            var query = new FilterEventsQuery
            {
                PageNumber = page,
                SearchTerm = search
            };

            var result = await _mediator.Send(query);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTickets(int eventId, int ticketCount)
        {
            try
            {
                await _mediator.Send(new AdminAddTicketCommand
                {
                    EventId = eventId,
                    TicketCount = ticketCount
                });

                TempData["Success"] = "Biletler başarıyla eklendi ve kapasite güncellendi.";
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                TempData["Error"] = "İşlem başarısız: " + inner;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SoldTickets(int eventId)
        {
            // Burası, o etkinliğe ait bilet listesini çeken servisi tetikleyecek yer.
            // Şimdilik view'ı döndürsün, içini daha sonra dolduralım.
            return View();
        }
    }
}
