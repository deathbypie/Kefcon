using AutoMapper;
using Kefcon.Dtos;
using Kefcon.Entities;

namespace Kefcon.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();
        }
    }
}