using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Categories.Queries;

namespace TicketBox.WebUI.ViewComponents
{
    public class CategoryBentoGridViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        public CategoryBentoGridViewComponent(IMediator mediator) => _mediator = mediator;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _mediator.Send(new GetCategoryQuery());
            return View(categories);
        }
    }
}