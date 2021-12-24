
using Social.Application.Features.Disabilities;
using Social.Application.Features.Hobbies;
using System.Collections.Generic;

namespace Social.Application.Features.Persons
{
    public class PersonViewModel
    {
        public string FirstName { get; set; }
        public string LasName { get; set; }
        public string Image { get; set; }
        public int InterestedId { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public List<HobbyViewModel> Hobbies { get; set; }
        public List<DisabilityViewModel> Disabilities { get; set; }

    }
}
