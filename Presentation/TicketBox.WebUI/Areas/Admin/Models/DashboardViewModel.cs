using TicketBox.Application.Features.Dashboard.Results;

namespace TicketBox.WebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public decimal TotalGrossSales { get; set; }
        public int ActiveEventsCount { get; set; }
        public int NewUsersCount { get; set; }
        public List<RecentTransactionItem> RecentTransactions { get; set; }
        public List<CategorySalesItem> CategorySales { get; set; } = new();
        public string AiInsightTitle { get; set; } = "Yapay Zeka Analizi";
        public string AiInsightDescription { get; set; } = "Veriler analiz ediliyor...";
    }

    public class CategorySalesItem
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal Percentage { get; set; }
    }
}
