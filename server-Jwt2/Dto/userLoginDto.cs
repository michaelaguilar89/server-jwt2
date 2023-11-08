using System.ComponentModel.DataAnnotations;

namespace server_Jwt2.Dto
{
    public class userLoginDto
    {
       
        public string username { get; set; }
       
        public string password { get; set; }
    }
}
