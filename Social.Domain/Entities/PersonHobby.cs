using Social.Domain.Common;

namespace Social.Domain.Entities
{
    public class PersonHobby : AuditableEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int HobbyId { get; set; }
        public virtual Hobby Hobby { get; set; }
    }
}
