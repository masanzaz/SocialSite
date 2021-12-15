using AutoMapper;
using MediatR;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Genres.Queries
{
    public class GetGenresAllQuery : IRequest<Response<IEnumerable<GenreViewModel>>>
    {
    }
    public class GetGenresAllQueryHandler : IRequestHandler<GetGenresAllQuery, Response<IEnumerable<GenreViewModel>>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetGenresAllQueryHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GenreViewModel>>> Handle(GetGenresAllQuery request, CancellationToken cancellationToken)
        {
            var genres = await _genreRepository.GetAllAsync();
            var genresViewModel = _mapper.Map<IEnumerable<GenreViewModel>>(genres);
            return new Response<IEnumerable<GenreViewModel>>(genresViewModel);
        }
    }
}
