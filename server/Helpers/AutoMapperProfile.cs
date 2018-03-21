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

            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();

            CreateMap<Timeslot, TimeslotDto>()
                .ForMember(
                dto => dto.EventId,
                timeslot => timeslot.MapFrom(t => t.Event.Id)
                );

            CreateMap<TimeslotDto, Timeslot>();

            CreateMap<Session, SessionDto>()
                .ForMember(
                dto => dto.TimeslotId,
                session => session.MapFrom(s => s.Time.Id)
                );
            CreateMap<SessionDto, Session>();
        }
    }
}