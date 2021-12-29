using Social.Domain.Common;
using System.Collections.Generic;

namespace Social.Domain.Entities
{
    public class Genre : AuditableEntity
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }

    }
}
