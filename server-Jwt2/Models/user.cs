using MongoDB.Bson.Serialization.Attributes;

namespace server_Jwt2.Models
{
    public class user
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRequired]
        public string? userName { get; set; }
        [BsonRequired]
        public string? role { get; set; }
        
        [BsonRequired]
        public byte[]? passwordSalt { get; set; }
        [BsonRequired]
        public byte[]? passwordHash { get; set; }
        [BsonRequired]
        public string? photo { get; set; }
    }
}
