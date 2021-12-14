using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces;
using Social.Application.Interfaces.Repositories;
using Social.Application.Parameters;
using Social.Application.Wrappers;
using Social.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Social.Application.Features.Disabilities.Queries
{
    public class GetDisabilitiesAllQuery : IRequest<PagedResponse<IEnumerable<DisabilityViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetDisabilitiesAllQuery, PagedResponse<IEnumerable<DisabilityViewModel>>>
    {
        private readonly IDisabilityRepository _disabilityRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IDisabilityRepository disabilityRepository, IMapper mapper)
        {
            _disabilityRepository = disabilityRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<DisabilityViewModel>>> Handle(GetDisabilitiesAllQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<RequestParameter>(request);
            var disabilities = await _disabilityRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var disabilityViewModel = _mapper.Map<IEnumerable<DisabilityViewModel>>(disabilities);
            var totalRecords = await _disabilityRepository.CountAsync();

            return new PagedResponse<IEnumerable<DisabilityViewModel>>(disabilityViewModel, validFilter.PageNumber, validFilter.PageSize, totalRecords);
        }
    }
}
