using Social.Domain.Common;

namespace Social.Domain.Entities
{
    public class Genre : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
