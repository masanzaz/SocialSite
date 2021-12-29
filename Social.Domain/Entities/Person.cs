using Social.Domain.Common;
using Social.Domain.Entities.Auth;
using System;
using System.Collections.Generic;

namespace Social.Domain.Entities
{
    public class Person : AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string FirstName { get; set; }
        public string LasName { get; set; }
        public string Image { get; set; }
        public string About { get; set; }
        public string City { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int InterestedId { get; set; }
        public virtual IList<PersonHobby> Hobbies { get; set; }
        public virtual IList<PersonDisability> Disabilities { get; set; }
        public virtual IList<Match> Receiver { get; set; }
        public virtual IList<Match> Senders { get; set; }
    }
}
