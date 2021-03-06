﻿using Kefcon.Data;
using Kefcon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Services
{
    public interface ITimeslotService : IServiceBase<Timeslot>
    {
        Timeslot Create(Timeslot timeslot, Guid eventId);
    }

    public class TimeslotService : ServiceBase<Timeslot>, ITimeslotService
    {
        private readonly IEventService _eventService;

        public TimeslotService(ApplicationDataContext context, IEventService eventService) : base(context)
        {
            _eventService = eventService;
        }

        public Timeslot Create(Timeslot timeslot, Guid eventId) {
            var eventEntity = _eventService.GetById(eventId);
            timeslot.Event = eventEntity;

            return Create(timeslot);
        }

        public override Timeslot Update(Timeslot entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var savedEntity = entities.SingleOrDefault(t => t.Id == entity.Id);

            savedEntity.StartTime = entity.StartTime;
            savedEntity.Duration = entity.Duration;

            return base.Update(savedEntity);
        }
    }
}
