using Social.Domain.Common;

namespace Social.Domain.Entities.Auth
{
   public class UserRole : AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
