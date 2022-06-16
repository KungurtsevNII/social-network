using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models;

public sealed class Register
{
    public sealed class Request
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public Profile Profile { get; set; }
    }
}