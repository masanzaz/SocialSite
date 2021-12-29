
using Social.Application.Features.Disabilities;
using Social.Application.Features.Hobbies;
using System.Collections.Generic;

namespace Social.Application.Features.Persons
{
    public class PersonViewModel
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LasName { get; set; }
        public string Image { get; set; }
        public string About { get; set; }
        public string City { get; set; }
        public int? Age { get; set; }
        public int InterestedId { get; set; }
        public int GenreId { get; set; }
        public int Distance { get; set; }
        public string GenreName { get; set; }
        public List<int> hobbies { get; set; }
        public List<int> disabilities { get; set; }

    }
}
