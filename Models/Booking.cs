using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AgileAPIAT2.Models
{
    public class Booking
    {
        // api model 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("booking_id")]
        public string BookingId { get; set; } = string.Empty;

        [BsonElement("size")]
        public int Size { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
