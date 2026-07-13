using TicketBox.Application.Features.Events.Results;

namespace TicketBox.WebUI.Models
{
    public class AiExperienceViewModel
    {
        public string MoodSuggestion { get; set; }
        public List<EventResult> SuggestedEvents { get; set; } // Önerilen etkinlikler
        public string AnalyticsInsight { get; set; } // Tabili AI'dan gelecek içgörü
    }
}
