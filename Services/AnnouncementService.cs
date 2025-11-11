// Using statement for MongoDB driver
using MongoDB.Driver;

// Service class to handle announcement-related operations with MongoDB
public class AnnouncementService
{
    // MongoDB collection for storing announcement documents
    private readonly IMongoCollection<Announcement> _announcements;

    // Constructor initializes the MongoDB collection
    public AnnouncementService(IMongoDatabase db)
    {
        _announcements = db.GetCollection<Announcement>("announcements");
    }

    // Retrieves all announcements from the database
    public async Task<List<Announcement>> GetAllAsync() =>
        await _announcements.Find(_ => true).ToListAsync();

    // Creates a new announcement in the database
    public async Task CreateAsync(Announcement a) =>
        await _announcements.InsertOneAsync(a);

    // Deletes an announcement by its ID
    public async Task DeleteAsync(string id) =>
        await _announcements.DeleteOneAsync(a => a.Id == id);
}
