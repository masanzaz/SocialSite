using Microsoft.EntityFrameworkCore;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly DbSet<Genre> _genre;
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _genre = dbContext.Set<Genre>();
        }

        public bool GenreExist(int genreId)
        {
            return  _genre.AnyAsync(x => x.Id == genreId).Result;
        }
    }
}
