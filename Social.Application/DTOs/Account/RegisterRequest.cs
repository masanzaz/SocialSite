using System.ComponentModel.DataAnnotations;


namespace Social.Application.DTOs.Account
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
