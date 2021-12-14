using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Disabilities.Queries
{
    public class GetDisabilityByIdQuery : IRequest<Response<DisabilityViewModel>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetDisabilityByIdQuery, Response<DisabilityViewModel>>
        {
            private readonly IDisabilityRepository _disabilityRepository;
            private readonly IMapper _mapper;
            public GetProductByIdQueryHandler(IDisabilityRepository disabilityRepository, IMapper mapper)
            {
                _disabilityRepository = disabilityRepository;
                _mapper = mapper;
            }
            public async Task<Response<DisabilityViewModel>> Handle(GetDisabilityByIdQuery query, CancellationToken cancellationToken)
            {
                Disability disability = await _disabilityRepository.GetByIdAsync(query.Id);
                var model = _mapper.Map<DisabilityViewModel>(disability);
                if (disability == null) throw new ApiException($"Disability Not Found.");
                return new Response<DisabilityViewModel> (model);
            }
        }



    }
}
