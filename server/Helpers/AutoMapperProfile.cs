using AutoMapper;
using Kefcon.Dtos;
using Kefcon.Entities;

namespace Kefcon.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();

            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();
        }
    }
}