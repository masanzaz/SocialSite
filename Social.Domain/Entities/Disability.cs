using Social.Domain.Common;

namespace Social.Domain.Entities
{
    public class Disability : AuditableEntity
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
