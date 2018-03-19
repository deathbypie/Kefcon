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

        public EventService(ApplicationDataContext context, IGameService gameService) : base(context)
        {
            _gameService = gameService;
        }

        public Session AddSession(Guid timeslotId, Guid gameId)
        {
            var timeslot = _context.Timeslots.SingleOrDefault(t => t.Id == timeslotId);
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
            _context.Sessions.Add(session);
            _context.SaveChanges();

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

            _context.Timeslots.Add(timeslot);
            _context.SaveChanges();

            return timeslot;
        }
    }
}
