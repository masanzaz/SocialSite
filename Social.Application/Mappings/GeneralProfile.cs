using AutoMapper;
using Social.Application.Features.Disabilities;
using Social.Application.Features.Disabilities.Queries;
using Social.Application.Parameters;
using Social.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Disability, DisabilityViewModel>().ReverseMap();
            CreateMap<GetDisabilitiesAllQuery, RequestParameter>();
        }
    }
}
