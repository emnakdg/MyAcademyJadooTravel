using AutoMapper;
using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookingCollection;
        private readonly IMongoCollection<Destination> _destinationCollection;
        private readonly IMapper _mapper;

        public BookingService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _bookingCollection = database.GetCollection<Booking>(databaseSettings.BookingCollectionName);
            _destinationCollection = database.GetCollection<Destination>(databaseSettings.DestinationCollectionName);
            _mapper = mapper;
        }

        public async Task CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var booking = _mapper.Map<Booking>(createBookingDto);

            //Toplam fiyatý hesapla
            var destination = await _destinationCollection.Find(x => x.DestinationId == createBookingDto.DestinationId).FirstOrDefaultAsync();
            if (destination != null)
            {
                booking.TotalPrice = destination.Price * createBookingDto.NumberOfPeople;
            }
            
            booking.Status = "Pending";
            booking.CreatedAt = DateTime.Now;
            
            await _bookingCollection.InsertOneAsync(booking);
        }

        public async Task DeleteBookingAsync(string id)
        {
            await _bookingCollection.DeleteOneAsync(x => x.BookingId == id);
        }

        public async Task<List<ResultBookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingCollection.Find(x => true).ToListAsync();
            var result = _mapper.Map<List<ResultBookingDto>>(bookings);

            foreach (var booking in result)
            {
                var destination = await _destinationCollection.Find(x => x.DestinationId == booking.DestinationId).FirstOrDefaultAsync();
                if (destination != null)
                {
                    booking.DestinationName = destination.CityCountry;
                }
            }
            
            return result;
        }

        public async Task<List<ResultBookingDto>> GetLatestBookingsAsync(int count)
        {
            var bookings = await _bookingCollection.Find(x => true)
                .SortByDescending(x => x.CreatedAt)
                .Limit(count)
                .ToListAsync();
            
            var result = _mapper.Map<List<ResultBookingDto>>(bookings);
            
            foreach (var booking in result)
            {
                var destination = await _destinationCollection.Find(x => x.DestinationId == booking.DestinationId).FirstOrDefaultAsync();
                if (destination != null)
                {
                    booking.DestinationName = destination.CityCountry;
                }
            }
            
            return result;
        }

        public async Task<GetByIdBookingDto> GetBookingByIdAsync(string id)
        {
            var booking = await _bookingCollection.Find(x => x.BookingId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdBookingDto>(booking);
        }

        public async Task UpdateBookingAsync(UpdateBookingDto updateBookingDto)
        {
            var booking = _mapper.Map<Booking>(updateBookingDto);
            await _bookingCollection.FindOneAndReplaceAsync(x => x.BookingId == updateBookingDto.BookingId, booking);
        }
    }
}
