// Using statements for MongoDB interaction and configuration
using Microsoft.Extensions.Options;
using MongoDB.Driver;

// Service class to handle booking-related operations with MongoDB
public class BookingService
{
    // MongoDB collection for storing booking documents
    private readonly IMongoCollection<Booking> _bookings;

    // Constructor initializes the MongoDB collection
    public BookingService(IMongoDatabase database)
    {
        _bookings = database.GetCollection<Booking>("bookings");
    }

    // Retrieves a paginated list of all bookings, sorted by date
    public async Task<List<Booking>> GetAllAsync(int skip = 0, int limit = 20)
    {
        return await _bookings
            .Find(_ => true)
            .SortByDescending(b => b.Date)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
    }

    // Gets the total count of bookings in the collection
    public async Task<long> GetCountAsync() =>
        await _bookings.CountDocumentsAsync(_ => true);

    // Retrieves bookings by their booking ID with a limit
    public async Task<List<Booking>> GetByBookingIdAsync(string bookingId, int limit = 24) =>
        await _bookings.Find(b => b.BookingId == bookingId)
               .Limit(limit)
               .ToListAsync();

    // Gets bookings by size with a specified limit
    public async Task<List<Booking>> GetBySizeAsync(int size, int limit = 24) =>
        await _bookings.Find(r => r.Size == size)
                .Limit(limit)
                .ToListAsync();

    // Retrieves a single booking by its MongoDB ID
    public async Task<Booking?> GetByIdAsync(string id) =>
        await _bookings.Find(r => r.Id == id).FirstOrDefaultAsync();

    // Creates a new booking with specified details
    public async Task CreateAsync(string bookingId, int size, DateTime date)
    {
        var booking = new Booking
        {
            Id = null, // MongoDB will generate this
            BookingId = bookingId,
            Size = size,
            Date = date
        };

        await _bookings.InsertOneAsync(booking);
    }

    // Updates an existing booking's size and date
    public async Task UpdateAsync(string id, int size, DateTime date)
    {
        var update = Builders<Booking>.Update
            .Set(b => b.Size, size)
            .Set(b => b.Date, date);

        await _bookings.UpdateOneAsync(b => b.Id == id, update);
    }

    // Deletes a booking by its ID
    public async Task DeleteAsync(string id) =>
        await _bookings.DeleteOneAsync(r => r.Id == id);

    // Commented out duplicate method
    // public async Task<List<Booking>> GetAllAsync(int limit = 20)
    // {
    //     return await _bookings.Find(_ => true).Limit(limit).ToListAsync();
    // }
}