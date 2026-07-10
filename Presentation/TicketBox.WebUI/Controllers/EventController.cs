using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Features.Events.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> EventList()
        {
            var values = await _mediator.Send(new GetEventQuery()); //getEventQueryHandler da çalışacak ve verileri alacak 
            return View(values);
        }

        public async Task<IActionResult> EventDetail()
        {
            return View();
        }
    }
}
