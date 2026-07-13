namespace TicketBox.Application.Features.Dashboard.Results
{
    public record DashboardQueryResult(
        decimal TotalGrossSales,
        int ActiveEventsCount,
        int CapacityUsagePercentage,
        int NewUsersCount,
        List<RecentTransactionItem> RecentTransactions,
        List<CategorySalesResult> CategorySales 
    );

    public record CategorySalesResult(string CategoryName, decimal Percentage);
    
    public record RecentTransactionItem(
        string UserName,
        string ActivityName,
        string Status,
        DateTime Time,
        string EventId
    );
}