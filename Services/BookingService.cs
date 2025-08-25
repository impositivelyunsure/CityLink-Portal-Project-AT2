using AgileAPIAT2.Models;
using MongoDB.Driver;

namespace AgileAPIAT2.Services
{
    public class BookingService
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingService(IMongoDatabase database)
        {
            _bookings = database.GetCollection<Booking>("bookings");
        }

        public async Task<List<Booking>> GetAllAsync(int skip = 0, int limit = 20)
        {
            return await _bookings
                .Find(_ => true)
                .Skip(skip)
                .Limit(limit)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(string id) =>
            await _bookings.Find(r => r.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Booking restaurant) =>
            await _bookings.InsertOneAsync(restaurant);

        public async Task UpdateAsync(string id, Booking restaurant) =>
            await _bookings.ReplaceOneAsync(r => r.Id == id, restaurant);

        public async Task DeleteAsync(string id) =>
            await _bookings.DeleteOneAsync(r => r.Id == id);

        public async Task<List<Booking>> GetAllAsync(int limit = 20)
        {
            return await _bookings.Find(_ => true).Limit(limit).ToListAsync();
        }
    }
}
