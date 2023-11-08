using MongoDB.Bson.Serialization.Attributes;

namespace server_Jwt2.Dto
{
    public class userRegisterDto
    {

        
        
        public string? userName { get; set; }
      
        public string? role { get; set; }

        public string? password { get; set; }
       
        public string? photo { get; set; }
       
    }
}
