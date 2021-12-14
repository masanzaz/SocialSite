using Microsoft.EntityFrameworkCore;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;


namespace Social.Infrastructure.Repositories
{
    public class DisabilityRepository : Repository<Disability>, IDisabilityRepository
    {
        private readonly DbSet<Disability> _disability;
        public DisabilityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _disability = dbContext.Set<Disability>();
        }




    }
}
