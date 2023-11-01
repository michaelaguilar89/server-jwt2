using System.ComponentModel.DataAnnotations;

namespace server_Jwt2.Dto
{
    public class userLoginDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
