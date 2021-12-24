using Social.Domain.Common;

namespace Social.Domain.Entities
{
    public class PersonDisability : AuditableEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int DisabilityId { get; set; }
        public virtual Disability Disability { get; set; }
    }
}
