using Social.Domain.Common;

namespace Social.Domain.Entities
{
    public class Match : AuditableEntity
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public virtual Person Sender { get; set; }
        public int ReceiverId { get; set; }
        public virtual Person Receiver { get; set; }
        public bool IsMatch { get; set; }
    }
}
