using Social.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Features.Messages
{
    public class LastMessagesViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Image { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public int UnreadCount { get; set; }
    }
}
