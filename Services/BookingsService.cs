using AT2CityLinkAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AT2CityLinkAPI.Services
{
    public class BookingsService
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingsService(IMongoDatabase database)
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

        public async Task<Booking> GetByName(string name)
        {
            return await _bookings.Find(r => r.Name == name).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateByNameAsync(string name, Booking updatedBooking)
        {
            var result = await _bookings.ReplaceOneAsync(
                b => b.Name == name,          
                updatedBooking              
            );

            return result.ModifiedCount > 0;  
        }

        public async Task<bool> UpdateBookingDateByNameAsync(string name, DateTime newDate)
        {
            var update = Builders<Booking>.Update
                .Set(b => b.Date, newDate);

            var result = await _bookings.UpdateOneAsync(
                b => b.Name == name,   // filter: find booking by Name
                update
            );

            return result.ModifiedCount > 0; // returns true if a record was updated
        }

        public async Task<List<Booking>> GetByPeopleAmount(string id) =>
            await _bookings.Find(r =>r.Id == id).ToListAsync();

    }
}
