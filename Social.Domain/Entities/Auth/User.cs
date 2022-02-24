using Social.Domain.Common;
using System.Collections.Generic;

namespace Social.Domain.Entities.Auth
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual IList<UserRole> Roles { get; set; }
    }
}
