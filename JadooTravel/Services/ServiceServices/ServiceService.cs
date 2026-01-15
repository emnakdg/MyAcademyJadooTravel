using AutoMapper;
using JadooTravel.Dtos.ServiceDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.ServiceServices
{
    public class ServiceService : IServiceService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Service> _serviceCollection;

        public ServiceService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _serviceCollection = database.GetCollection<Service>(_databaseSettings.ServiceCollectionName);
            _mapper = mapper;
        }

        public async Task CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            var value = _mapper.Map<Service>(createServiceDto);
            await _serviceCollection.InsertOneAsync(value);
        }

        public async Task DeleteServiceAsync(string id)
        {
            await _serviceCollection.DeleteOneAsync(x => x.ServiceId == id);
        }

        public async Task<List<ResultServiceDto>> GetAllServiceAsync()
        {
            var values = await _serviceCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultServiceDto>>(values);
        }

        public async Task<GetServiceByIdDto> GetServiceByIdAsync(string id)
        {
            var value = await _serviceCollection.Find(x => x.ServiceId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetServiceByIdDto>(value);
        }

        public async Task UpdateServiceAsync(UpdateServiceDto updateServiceDto)
        {
            var value = _mapper.Map<Service>(updateServiceDto);
            await _serviceCollection.FindOneAndReplaceAsync(x => x.ServiceId == updateServiceDto.ServiceId, value);
        }
    }
}
