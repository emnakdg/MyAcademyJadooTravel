using JadooTravel.Dtos.BookingDtos;

namespace JadooTravel.Services.BookingServices
{
    public interface IBookingService
    {
        Task<List<ResultBookingDto>> GetAllBookingsAsync();
        Task<List<ResultBookingDto>> GetLatestBookingsAsync(int count);
        Task CreateBookingAsync(CreateBookingDto createBookingDto);
        Task UpdateBookingAsync(UpdateBookingDto updateBookingDto);
        Task DeleteBookingAsync(string id);
        Task<GetByIdBookingDto> GetBookingByIdAsync(string id);
    }
}
