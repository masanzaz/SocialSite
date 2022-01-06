using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Features.Matches
{
    public class MatchViewModel
    {
        public int MatchId { get; set; }
        public int id { get; set; }
        public int? Age { get; set; }
        public string FirstName { get; set; }
        public string LasName { get; set; }
        public string Image { get; set; }
        public string MatchImage { get; set; }
        public string About { get; set; }
        public string City { get; set; }

    }
}
