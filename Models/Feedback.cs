using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Feedback
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("message")]
    public string Message { get; set; } = null!;

    [BsonElement("dateSubmitted")]
    public DateTime DateSubmitted { get; set; } = DateTime.Now;
}
