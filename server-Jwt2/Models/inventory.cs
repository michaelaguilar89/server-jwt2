using MongoDB.Bson.Serialization.Attributes;

namespace server_Jwt2.Models
{
    public class inventory
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string name { get; set; }
        [BsonRequired]
        public string description { get; set; }
        [BsonRequired]
        public double quantity { get; set; }
        [BsonRequired]
        public double price { get; set; }
        [BsonRequired]
        public string CategoryId { get; set; }
        [BsonRequired]
        public string photo { get; set; }
    }
}
