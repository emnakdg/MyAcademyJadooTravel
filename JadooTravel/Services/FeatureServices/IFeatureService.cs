using JadooTravel.Dtos.FeatureDtos;

namespace JadooTravel.Services.FeatureServices
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDto>> GetAllFeatureAsync();
        Task<GetFeatureByIdDto> GetFeatureByIdAsync(string id);
        Task CreateFeatureAsync(CreateFeatureDto createFeatureDto);
        Task DeleteFeatureAsync(string id);
        Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto);
    }
}
