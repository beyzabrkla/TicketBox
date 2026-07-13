using System.Text.Json.Serialization;

namespace TicketBox.Application.Interfaces
{
    public class MoodRecommendation
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string CategoryName { get; set; } = string.Empty;
    }

    public interface IAiService
    {
        Task<MoodRecommendation> GetMoodBasedRecommendationAsync(string mood, List<string> availableCategories);
        Task<string> GetAnalyticsInsightAsync(string userQuestion, string salesDataSummary);
    }
}