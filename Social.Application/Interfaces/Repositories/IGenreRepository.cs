using Social.Domain.Entities;
using System.Threading.Tasks;

namespace Social.Application.Interfaces.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        bool GenreExist(int genreId);
    }
}
