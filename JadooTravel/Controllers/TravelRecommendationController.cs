using JadooTravel.Services.AiServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class TravelRecommendationController : Controller
    {
        private readonly IAiService _aiService;

        public TravelRecommendationController(IAiService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost]
        [Route("api/recommendations")]
        public async Task<IActionResult> GetRecommendations([FromBody] TravelRecommendationRequest request)
        {
            if (string.IsNullOrEmpty(request.City) || string.IsNullOrEmpty(request.Country))
            {
                return BadRequest(new { success = false, message = "Şehir ve ülke bilgisi gereklidir." });
            }

            try
            {
                var recommendations = await _aiService.GetTravelRecommendationsAsync(request.City, request.Country);
                return Ok(new { success = true, recommendations = recommendations });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }

    public class TravelRecommendationRequest
    {
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
    }
}
