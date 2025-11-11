using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

// Service class to handle feedback-related operations with MongoDB
public class FeedbackService
{
    // MongoDB collection for storing feedback documents
    private readonly IMongoCollection<Feedback> _feedback;

    // Constructor initializes the MongoDB collection
    public FeedbackService(IMongoDatabase db)
    {
        _feedback = db.GetCollection<Feedback>("feedback");
    }

    // Retrieves all feedback entries, sorted by submission date
    public async Task<List<Feedback>> GetAllAsync() =>
        await _feedback.Find(_ => true).SortByDescending(f => f.DateSubmitted).ToListAsync();

    // Inserts a new feedback entry with current UTC timestamp
    public async Task InsertAsync(Feedback feedback)
    {
        feedback.DateSubmitted = DateTime.UtcNow;
        await _feedback.InsertOneAsync(feedback);
    }
}
