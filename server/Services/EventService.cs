using Kefcon.Data;
using Kefcon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Services
{
    public interface IEventService : IServiceBase<Event>
    {
        Timeslot AddTimeslot(Guid eventId, DateTime startTime, TimeSpan span);
        Session AddSession(Guid timeslotId, Guid gameId);
    }

    public class EventService : ServiceBase<Event>, IEventService
    {
        private readonly IGameService _gameService;
        private readonly ITimeslotService _timeslotService;
        private readonly ISessionService _sessionService;

        public EventService(
            ApplicationDataContext context, 
            IGameService gameService, 
            ITimeslotService timeslotService,
            ISessionService sessionService) : base(context)
        {
            _gameService = gameService;
            _timeslotService = timeslotService;
            _sessionService = sessionService;
        }

        public Session AddSession(Guid timeslotId, Guid gameId)
        {
            var timeslot = _timeslotService.GetById(timeslotId);
            if (timeslot == null)
            {
                throw new ArgumentNullException("timeslot");
            }

            var game = _gameService.GetById(gameId);
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            var session = new Session
            {
                Time = timeslot,
                Game = game
            };

            session.Time = timeslot;
            _sessionService.Create(session);

            return session;
        }

        public Timeslot AddTimeslot(Guid eventId, DateTime startTime, TimeSpan span)
        {
            var eventEntity = entities.SingleOrDefault(e => e.Id == eventId);

            if (eventEntity == null)
            {
                throw new ArgumentNullException("event");
            }

            var timeslot = new Timeslot
            {
                Id = Guid.NewGuid(),
                StartTime = startTime,
                Duration = span,
                Sessions = new List<Session>()
            };

            timeslot.Event = eventEntity;
            _timeslotService.Create(timeslot);

            return timeslot;
        }
    }
}
