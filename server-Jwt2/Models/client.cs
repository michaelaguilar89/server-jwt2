using MongoDB.Bson.Serialization.Attributes;

namespace server_Jwt2.Models
{
    public class client
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name"),BsonRequired]
        public string? firstName { get; set; }
        [BsonElement("lastname"),BsonRequired]
        public string? lastName { get; set; }
        [BsonRequired]
        public int? age { get; set; }
        [BsonRequired]
        public decimal? salary { get; set; }
        [BsonRequired]
        public string? country { get; set; }
        [BsonRequired]
        public string? photo { get; set; }
    }
}
