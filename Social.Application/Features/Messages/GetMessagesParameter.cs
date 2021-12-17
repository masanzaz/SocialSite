using Social.Application.Parameters;

namespace Social.Application.Features.Messages
{
    public class GetMessagesParameter : RequestParameter
    {
        public int MatchId { get; set; }
    }
}
