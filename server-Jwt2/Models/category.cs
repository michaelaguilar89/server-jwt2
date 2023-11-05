using MongoDB.Bson.Serialization.Attributes;

namespace server_Jwt2.Models
{
    public class category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string name { get; set; }
    }
}
