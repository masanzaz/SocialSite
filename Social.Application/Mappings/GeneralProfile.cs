using AutoMapper;
using Social.Application.Features.Disabilities;
using Social.Application.Features.Disabilities.Queries;
using Social.Application.Features.Genres;
using Social.Application.Features.Hobbies;
using Social.Application.Features.Persons.Commands;
using Social.Application.Features.Users.Commands;
using Social.Application.Parameters;
using Social.Domain.Entities;
using Social.Domain.Entities.Auth;

namespace Social.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Disability, DisabilityViewModel>().ReverseMap();
            CreateMap<GetDisabilitiesAllQuery, RequestParameter>();
            CreateMap<Genre, GenreViewModel>().ReverseMap();
            CreateMap<Hobby, HobbyViewModel>().ReverseMap();
            CreateMap<CreatePersonCommand, Person>()
                .ForMember(x => x.Hobbies, y => y.Ignore());
            CreateMap<CreateUserCommand, User>()
                .ForMember(x => x.Roles, y => y.Ignore());
        }
    }
}
