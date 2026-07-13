using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TicketBox.Application.Interfaces;

namespace TicketBox.Persistance.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _geminiApiKey;
        private readonly string _tavilyApiKey;
        public AiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _geminiApiKey = configuration["GeminiSettings:ApiKey"]
                      ?? throw new ArgumentNullException("Gemini API Key bulunamadı!");

            _tavilyApiKey = configuration["TavilySettings:ApiKey"]?.Trim()
                             ?? throw new ArgumentNullException("Tavily API Key bulunamadı!");
        }

        public async Task<MoodRecommendation> GetMoodBasedRecommendationAsync(string mood, List<string> availableCategories)
        {
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash:generateContent";

            var categoryList = string.Join(", ", availableCategories);

            var prompt = $@"Kullanıcı şu an '{mood}' modunda hissediyor.

                Kullanabileceğin kategoriler TAM OLARAK şunlar (başka hiçbir kategori adı kullanma, bunlardan birini harfi harfine seç):
                {categoryList}

                Kurallar:
                - ""category"" alanına yukarıdaki listeden BİREBİR AYNI yazımla bir tanesini yaz (büyük/küçük harf ve tire dahil aynen kopyala).
                - Kullanıcının modu için en uygun kategoriyi seç (örneğin stresli/yorgun modda rahatlatıcı bir tür, enerjik modda hareketli bir tür seç).
                - ""message"" alanına kısa, samimi, emoji içerebilen bir öneri metni yaz; bu metin seçtiğin kategoriyle TUTARLI olmalı (mesajda bahsettiğin etkinlik türü ile category alanı aynı şey olmalı).
                - JSON'dan önce veya sonra KESİNLİKLE hiçbir kelime, açıklama veya ek karakter ekleme. Cevabın SADECE ve SADECE o JSON objesi olsun.
                - SADECE aşağıdaki JSON formatında cevap ver, markdown code fence (```) kullanma:

                {{""category"": ""LİSTEDEN_BİREBİR_KOPYALANMIŞ_KATEGORİ"", ""message"": ""ÖNERİ_METNİ""}}";

            var requestBody = new
            {
                contents = new[] {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            request.Headers.Add("x-goog-api-key", _geminiApiKey);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return new MoodRecommendation { Message = "Şu an AI servisine bağlanılamıyor.", CategoryName = "" };

            using var doc = JsonDocument.Parse(result);
            var rawText = doc.RootElement
                              .GetProperty("candidates")[0]
                              .GetProperty("content")
                              .GetProperty("parts")[0]
                              .GetProperty("text")
                              .GetString() ?? "";

            rawText = rawText.Trim();

            int firstBrace = rawText.IndexOf('{');
            int lastBrace = rawText.LastIndexOf('}');

            if (firstBrace == -1 || lastBrace == -1 || lastBrace < firstBrace)
                return new MoodRecommendation { Message = rawText, CategoryName = "" };

            var jsonPart = rawText.Substring(firstBrace, lastBrace - firstBrace + 1);

            try
            {
                var parsed = JsonSerializer.Deserialize<MoodRecommendation>(
                    jsonPart,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return parsed ?? new MoodRecommendation { Message = "Öneri oluşturulamadı.", CategoryName = "" };
            }
            catch (JsonException)
            {
                return new MoodRecommendation { Message = rawText, CategoryName = "" };
            }
        }

        public async Task<string> GetAnalyticsInsightAsync(string userQuestion, string salesDataSummary)
        {
            var url = "https://api.tavily.com/search";

            var truncatedData = salesDataSummary.Length > 200 ? salesDataSummary.Substring(0, 200) + "..." : salesDataSummary;

            var query = $"Veri: {truncatedData}. Soru: {userQuestion}. TALİMAT: Profesyonel veri analisti olarak; verileri net, hiyerarşik (yüksekten düşüğe) ve iş odaklı analiz et. Sadece Türkçe yanıtla.";
            var requestBody = new
            {
                query = query,
                search_depth = "basic",
                include_answer = true
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            request.Headers.Add("Authorization", $"Bearer {_tavilyApiKey}");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                return $"Tavily Hatası ({response.StatusCode}): {errorDetails}";
            }

            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);

            return doc.RootElement.TryGetProperty("answer", out var answer)
                ? answer.GetString()
                : "Cevap oluşturulamadı.";
        }
    }
}