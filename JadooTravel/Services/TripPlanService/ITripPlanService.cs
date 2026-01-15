using JadooTravel.Dtos.TripPlanDtos;

namespace JadooTravel.Services.TripPlanService
{
    public interface ITripPlanService
    {
        Task<List<ResultTripPlanDto>> GetAllTripPlanAsync();
        Task<GetTripPlanByIdDto> GetTripPlanByIdAsync(string id);
        Task CreateTripPlanAsync(CreateTripPlanDto createTripPlanDto);
        Task DeleteTripPlanAsync(string id);
        Task UpdateTripPlanAsync(UpdateTripPlanDto updateTripPlanDto);
    }
}
