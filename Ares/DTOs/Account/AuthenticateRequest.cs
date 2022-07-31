using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class AuthenticateRequest
    {
        [Required]
        public string Login {get; set;}
        [Required]
        public string Password { get; set; }
    }
}