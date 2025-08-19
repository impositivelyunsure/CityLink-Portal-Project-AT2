using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AT2CityLinkAPI.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? Id { get; set; }

        [BsonElement("booking_id")]
        public string BookingId { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("price")]
        public double Price { get; set; } = double.MinValue;

        [BsonElement("people_amount")]
        public int PeopleAmount { get; set; } = int.MinValue;

        [BsonElement("date")]
        public DateTime? Date { get; set; }
    }
}
