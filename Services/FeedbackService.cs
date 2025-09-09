using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FeedbackService
{
    private readonly IMongoCollection<Feedback> _feedback;

    public FeedbackService(IMongoDatabase db)
    {
        _feedback = db.GetCollection<Feedback>("feedback");
    }

    public async Task<List<Feedback>> GetAllAsync() =>
        await _feedback.Find(_ => true).SortByDescending(f => f.DateSubmitted).ToListAsync();

    public async Task InsertAsync(Feedback feedback)
    {
        feedback.DateSubmitted = DateTime.UtcNow;
        await _feedback.InsertOneAsync(feedback);
    }
}
