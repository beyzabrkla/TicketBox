using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Bookings.Commands;
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

        public async Task<IActionResult> EventList(string? searchTerm, int? categoryId, int pageNumber = 1, int pageSize = 6)
        {
            var categoryResult = await _mediator.Send(new GetCategoryQuery());

            ViewBag.Categories = categoryResult;

            //Query hazırlanması
            var query = new FilterEventsQuery
            {
                SearchTerm = searchTerm,
                CategoryId = categoryId,
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
        public async Task<IActionResult> BuyTicket(int eventId, int ticketCount)
        {
            try
            {
                var command = new CreateBookingCommand
                {
                    EventId = eventId,
                    TicketCount = ticketCount
                };

                await _mediator.Send(command);

                return RedirectToAction("MyTickets", "Profile");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return RedirectToAction("EventDetail", "Event", new { id = eventId });
            }
        }
    }
}