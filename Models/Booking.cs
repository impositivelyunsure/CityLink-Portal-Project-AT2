using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public class Booking
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [BsonElement("notes")]
    public string Notes { get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

}
