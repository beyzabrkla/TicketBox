using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Dashboard.Queries;
using TicketBox.Application.Interfaces;
using TicketBox.WebUI.Areas.Admin.Models;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAiService _aiService;

        public DashboardController(IMediator mediator, IAiService aiService)
        {
            _mediator = mediator;
            _aiService = aiService;
        }

        public async Task<IActionResult> Index()
        {
            var queryResult = await _mediator.Send(new GetDashboardQuery());

            // İlk açılışta genel bir özet sunalım
            var salesSummary = string.Join(", ", queryResult.CategorySales.Select(x => $"{x.CategoryName}: %{Math.Round(x.Percentage)}"));
            string initialInsight = await _aiService.GetAnalyticsInsightAsync("Satış verilerimi kısaca analiz et.", salesSummary);

            var viewModel = new DashboardViewModel
            {
                TotalGrossSales = queryResult.TotalGrossSales,
                ActiveEventsCount = queryResult.ActiveEventsCount,
                NewUsersCount = queryResult.NewUsersCount,
                RecentTransactions = queryResult.RecentTransactions,
                CategorySales = queryResult.CategorySales.Select(x => new CategorySalesItem { CategoryName = x.CategoryName, Percentage = Math.Round(x.Percentage, 1) }).ToList(),
                AiInsightTitle = "Tavily AI Veri Analitiği",
                AiInsightDescription = initialInsight
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AskAi(string userQuestion)
        {
            var queryResult = await _mediator.Send(new GetDashboardQuery());
            var salesSummary = string.Join(", ", queryResult.CategorySales.Select(x => $"{x.CategoryName}: %{Math.Round(x.Percentage)}"));

            var answer = await _aiService.GetAnalyticsInsightAsync(userQuestion, salesSummary);
            return Json(new { answer = answer });
        }
    }
}