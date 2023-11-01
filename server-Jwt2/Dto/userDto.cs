using MongoDB.Bson.Serialization.Attributes;

namespace server_Jwt2.Dto
{
    public class userDto
    {

        public string? Id { get; set; }
        
        public string? userName { get; set; }
      
        public string? role { get; set; }

        public string? password { get; set; }
       
        public string? photo { get; set; }
        public string? token { get; set; }
        public DateTime? expirationTime { get; set; }
    }
}
