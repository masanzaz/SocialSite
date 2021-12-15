using Microsoft.EntityFrameworkCore;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;

namespace Social.Infrastructure.Repositories
{
    public class HobbyRepository : Repository<Hobby>, IHobbyRepository
    {
        private readonly DbSet<Hobby> _disability;
        public HobbyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _disability = dbContext.Set<Hobby>();
        }
    }
}
