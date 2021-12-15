using Social.Domain.Common;

namespace Social.Domain.Entities
{
    public class Hobby : AuditableEntity
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
    }
}
