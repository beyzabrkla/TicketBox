using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Application.Features.Bookings.Queries;
using TicketBox.Application.Features.Categories.Queries;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.ViewModels;
using TicketBox.Application.Interfaces;

namespace TicketBox.WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;

        public EventController(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<IActionResult> EventList(string? search, int? categoryId, decimal? maxPrice, int pageNumber = 1, int pageSize = 6)
        {
            var categoryResult = await _mediator.Send(new GetCategoryQuery());

            ViewBag.Categories = categoryResult;

            //Query hazırlanması
            var query = new FilterEventsQuery
            {
                SearchTerm = search,
                CategoryId = categoryId,
                MaxPrice = maxPrice, 
                PageNumber = pageNumber,
                PageSize = pageSize,
                IsActive = true,
                SoldOut = false,
                Upcoming = true
            };

            var result = await _mediator.Send(query);

            //ViewModeli doldur
            var viewModel = new EventListViewModel
            {
                Events = result.Items,
                TotalCount = result.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EventDetail(int id)
        {
            // Mediator ile veriyi çek
            var query = new GetByIdEventQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(); // 404 hatası döner
            }

            return View(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BuyTicket(CreateBookingCommand command)
        {
            // Bilet oluşturulduğunda dönen 'bookingId'yi yakalıyoruz
            var bookingId = await _mediator.Send(command);

            // Artık kullanıcıyı MyTickets'e değil, biletin görüntülendiği Confirmation sayfasına gönderiyoruz
            return RedirectToAction("Confirmation", "Event", new { bookingId = bookingId });
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int bookingId)
        {
            // Veritabanından o booking'e ait bilgileri ve biletleri çek
            // Burada daha önce hazırladığın Bilet Tasarımını (HTML) bu view'a model olarak gönder
            var result = await _mediator.Send(new GetBookingDetailsQuery { Id = bookingId });
            return View(result);
        }
    }
}