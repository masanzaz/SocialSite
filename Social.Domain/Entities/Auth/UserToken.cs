using Social.Domain.Common;
using System;

namespace Social.Domain.Entities.Auth
{
    public class UserToken
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
