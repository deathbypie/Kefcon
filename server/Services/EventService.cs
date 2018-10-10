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
    }

    public class EventService : ServiceBase<Event>, IEventService
    {

        public EventService(
            ApplicationDataContext context) : base(context)
        {
        }

        public override Event Update(Event entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var savedEntity = entities.SingleOrDefault(t => t.Id == entity.Id);

            savedEntity.Name = entity.Name;

            return base.Update(savedEntity);
        }
    }
}
