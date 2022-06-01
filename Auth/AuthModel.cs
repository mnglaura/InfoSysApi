using System.ComponentModel.DataAnnotations;

namespace infosysapi.Auth
{
    public class AuthModel
    {
        [Required]
        public string username { get; set; }
        
        [Required]
        public string password { get; set; }
    }
}