using Social.Application.Interfaces;
using System;
namespace Social.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
