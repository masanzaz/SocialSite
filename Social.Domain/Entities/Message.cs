using Social.Domain.Common;
using Social.Domain.Entities.Auth;
using Social.Domain.Enums;

namespace Social.Domain.Entities
{
    public class Message : AuditableEntity
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int SenderId { get; set; }
        public virtual Match Match { get; set; }
        public MessageStatus Status { get; set; }
        public string Content { get; set; }

    }
}
