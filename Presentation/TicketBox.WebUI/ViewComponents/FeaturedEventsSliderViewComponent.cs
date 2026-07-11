using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Events.Queries;

namespace TicketBox.WebUI.ViewComponents
{
    public class FeaturedEventsSliderViewComponent :ViewComponent
    {
        private readonly IMediator _mediator;

        public FeaturedEventsSliderViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count = 6)
        {
            // İsteğe bağlı bir Query ile popüler etkinlikleri çekiyoruz
            var events = await _mediator.Send(new GetPopularEventsQuery(count));
            return View(events);
        }
    }
}
