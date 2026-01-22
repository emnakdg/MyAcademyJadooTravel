using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JadooTravel.Services.AiServices
{
    public interface IAiService
    {
        Task<string> GetTravelRecommendationsAsync(string city, string country);
    }

    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"] ?? "";
        }

        public async Task<string> GetTravelRecommendationsAsync(string city, string country)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "API anahtarı yapılandırılmamış. Lütfen appsettings.json dosyasına OpenAI API anahtarınızı ekleyin.";
            }

            try
            {
                var prompt = $"'{city}, {country}' şehrinde gezilmesi gereken en önemli 10 yeri listele. Her yer için kısa bir açıklama ekle. Türkçe olarak yanıtla ve madde işaretleri kullan.";

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen bir seyahat rehberisin. Kullanıcılara şehirlerde gezilecek yerler hakkında kısa ve bilgilendirici öneriler sunuyorsun." },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 1000,
                    temperature = 0.7
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    using var doc = JsonDocument.Parse(responseContent);
                    var message = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

                    return message ?? "Öneri bulunamadı.";
                }
                else
                {
                    return $"API hatası: {response.StatusCode}. Lütfen API anahtarınızı kontrol edin.";
                }
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }
    }
}
