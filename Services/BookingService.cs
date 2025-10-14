using Microsoft.Extensions.Options;
using MongoDB.Driver;

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
            .SortByDescending(b => b.Date)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
    }

    public async Task<long> GetCountAsync() =>
        await _bookings.CountDocumentsAsync(_ => true);


    public async Task<List<Booking>> GetByBookingIdAsync(string bookingId, int limit = 24) =>
        await _bookings.Find(b => b.BookingId == bookingId)
               .Limit(limit)
               .ToListAsync();

    public async Task<List<Booking>> GetBySizeAsync(int size, int limit = 24) =>
        await _bookings.Find(r => r.Size == size)
                .Limit(limit)
                .ToListAsync();

    public async Task<Booking?> GetByIdAsync(string id) =>
        await _bookings.Find(r => r.Id == id).FirstOrDefaultAsync();



    public async Task CreateAsync(string bookingId, int size, DateTime date)
    {
        var booking = new Booking
        {
            Id = null,
            BookingId = bookingId,
            Size = size,
            Date = date
        };

        await _bookings.InsertOneAsync(booking);
    }

    public async Task UpdateAsync(string id, int size, DateTime date)
    {
        var update = Builders<Booking>.Update
            .Set(b => b.Size, size)
            .Set(b => b.Date, date);

        await _bookings.UpdateOneAsync(b => b.Id == id, update);
    }

    public async Task DeleteAsync(string id) =>
        await _bookings.DeleteOneAsync(r => r.Id == id);

    // public async Task<List<Booking>> GetAllAsync(int limit = 20)
    // {
    //     return await _bookings.Find(_ => true).Limit(limit).ToListAsync();
    // }
}