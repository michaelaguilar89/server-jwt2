using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server_Jwt2.Models
{
    public class sales
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public DateTime saleDate { get; set; }
        [BsonRequired]
        public string UserId { get; set; }
        [BsonRequired]
        public string ClientId { get; set; }
        [BsonRequired]
        public List<SoldProduct> ProductsSold { get; set; }
        public double total { get; set; }

    }
    public class SoldProduct

    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string SoldProductId { get; set; }
        public string SoldProductName { get; set; }
        public double SoldProductQuantity { get; set; }
        public double SoldProductPrice { get; set; }
        public double SoldProductTotal { get; set; }

    }
}
