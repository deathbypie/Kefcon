﻿using AutoMapper;
using Kefcon.Dtos;
using Kefcon.Entities;
using Kefcon.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Controllers
{
    [Route("api/[controller]")]
    public class EventController : BaseController
    {
        private IEventService _eventService;

        public EventController(IEventService eventService, IMapper mapper) : base(mapper)
        {
            _eventService = eventService;
        }
        [HttpGet]
        public IEnumerable<EventDto> GetAll()
        {
            var events = _eventService.GetAll();
            var eventDtos = events.Select(g => _mapper.Map<EventDto>(g));
            return eventDtos;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        { 
            var eventEntity = _eventService.GetById(id);
            var eventDto = _mapper.Map<EventDto>(eventEntity);
            return Ok(eventDto);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]EventDto eventDto)
        {
            eventDto.Id = Guid.NewGuid();

            var eventEntity = _mapper.Map<Event>(eventDto);

            _eventService.Create(eventEntity);

            return Ok(eventDto);
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody]EventDto eventDto)
        {
            if (eventDto == null || eventDto.Id == Guid.Empty)
            {
                return BadRequest("Event not found.");
            }
            var eventEntity = _mapper.Map<Event>(eventDto);

            _eventService.Update(eventEntity);

            return Ok(eventDto);
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(Guid id)
        {
            _eventService.Delete(id);

            return Ok();
        }
    }
}
