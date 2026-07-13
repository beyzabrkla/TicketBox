using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Interfaces;
using TicketBox.Application.Features.Categories.Queries;
using TicketBox.Application.Features.Events.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class ChatController : Controller
    {
        private readonly IAiService _aiService;
        private readonly IMediator _mediator;

        public ChatController(IAiService aiService, IMediator mediator)
        {
            _aiService = aiService;
            _mediator = mediator;
        }

        public IActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessMood(string userMood)
        {
            var categories = await _mediator.Send(new GetCategoryQuery());
            var categoryNames = categories.Select(c => c.CategoryName).ToList();

            var recommendation = await _aiService.GetMoodBasedRecommendationAsync(userMood, categoryNames);

            var matchedCategory = categories.FirstOrDefault(c =>
                string.Equals(c.CategoryName, recommendation.CategoryName, StringComparison.OrdinalIgnoreCase));

            // Birebir eşleşme yoksa, kısmi eşleşmeyi dene (ör. AI "Stand-Up Gösterisi" derse "Stand-Up" ile eşleşsin)
            if (matchedCategory == null && !string.IsNullOrWhiteSpace(recommendation.CategoryName))
            {
                matchedCategory = categories.FirstOrDefault(c =>
                    recommendation.CategoryName.Contains(c.CategoryName, StringComparison.OrdinalIgnoreCase) ||
                    c.CategoryName.Contains(recommendation.CategoryName, StringComparison.OrdinalIgnoreCase));
            }

            var filterQuery = new FilterEventsQuery
            {
                CategoryId = matchedCategory?.CategoryId,
                IsActive = true,
                Upcoming = true,
                SoldOut = false,
                PageNumber = 1,
                PageSize = 6
            };

            var eventResult = await _mediator.Send(filterQuery);

            ViewBag.AiResponse = recommendation.Message;
            ViewBag.UserMood = userMood;
            ViewBag.MatchedCategory = matchedCategory?.CategoryName;

            return View("Result", eventResult.Items);
        }
    }
}