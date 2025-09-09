using MongoDB.Driver;

public class AnnouncementService
{
    private readonly IMongoCollection<Announcement> _announcements;

    public AnnouncementService(IMongoDatabase db)
    {
        _announcements = db.GetCollection<Announcement>("announcements");
    }

    public async Task<List<Announcement>> GetAllAsync() =>
        await _announcements.Find(_ => true).ToListAsync();

    public async Task CreateAsync(Announcement a) =>
        await _announcements.InsertOneAsync(a);

    public async Task DeleteAsync(string id) =>
        await _announcements.DeleteOneAsync(a => a.Id == id);
}
