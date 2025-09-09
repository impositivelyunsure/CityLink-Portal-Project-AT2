using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Announcement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("content")]
    public string Content { get; set; } = null!;

    [BsonElement("date")]
    public DateTime Date { get; set; } = DateTime.Now;
}
