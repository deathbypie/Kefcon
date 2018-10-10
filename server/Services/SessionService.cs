using Kefcon.Data;
using Kefcon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Services
{
    public interface ISessionService : IServiceBase<Session>
    {
        Session Create(Session session, Guid timeslotId);
    }

    public class SessionService : ServiceBase<Session>, ISessionService
    {
        private readonly ITimeslotService _timeslotService;

        public SessionService(ApplicationDataContext context, ITimeslotService timeslotService) : base(context)
        {
            _timeslotService = timeslotService;
        }

        public Session Create(Session session, Guid timeslotId)
        {
            var timeslot = _timeslotService.GetById(timeslotId);
            session.Time = timeslot;

            return Create(session);
        }

        public override Session Update(Session entity)
        {
            throw new NotImplementedException("Cannot update sessions directly.");
        }
    }
}
