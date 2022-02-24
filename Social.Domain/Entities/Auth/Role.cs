using Social.Domain.Common;

namespace Social.Domain.Entities.Auth
{
    public class Role : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
    }
}
