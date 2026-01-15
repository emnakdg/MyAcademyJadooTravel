using JadooTravel.Dtos.ReservationDtos;

namespace JadooTravel.Services.ReservationServices
{
    public interface IReservationService
    {
        Task<List<ResultReservationDto>> GetAllReservationAsync();
        Task<GetReservationByIdDto> GetReservationByIdAsync(string id);
        Task CreateReservationAsync(CreateReservationDto createReservationDto);
        Task DeleteReservationAsync(string id);
        Task UpdateReservationAsync(UpdateReservationDto updateReservationDto);
    }
}
