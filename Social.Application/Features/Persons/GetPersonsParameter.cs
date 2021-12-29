using Social.Application.Parameters;

namespace Social.Application.Features.Persons
{
    public class GetPersonsParameter : RequestParameter
    {
        public int PersonId { get; set; }
    }
}
