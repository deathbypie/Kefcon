using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kefcon.Dtos;
using Kefcon.Entities;
using Kefcon.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kefcon.Controllers
{
    public class SessionController : BaseController
    {
        private ISessionService _sessionService;

        public SessionController(ISessionService sessionService, IMapper mapper) : base(mapper)
        {
            _sessionService = sessionService;
        }
        [HttpGet]
        public IEnumerable<SessionDto> GetAll()
        {
            var sessions = _sessionService.GetAll();
            var sessionDtos = sessions.Select(g => _mapper.Map<SessionDto>(g));
            return sessionDtos;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var sessionEntity = _sessionService.GetById(id);
            var sessionDto = _mapper.Map<SessionDto>(sessionEntity);
            return Ok(sessionDto);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]SessionDto sessionDto)
        {
            sessionDto.Id = Guid.NewGuid();

            var sessionEntity = _mapper.Map<Session>(sessionDto);

            _sessionService.Create(sessionEntity, sessionDto.TimeslotId);

            return Ok(sessionDto);
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody]SessionDto sessionDto)
        {
            if (sessionDto == null || sessionDto.Id == Guid.Empty)
            {
                return BadRequest("Session not found.");
            }
            var sessionEntity = _mapper.Map<Session>(sessionDto);

            _sessionService.Update(sessionEntity);

            return Ok(sessionDto);
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(Guid id)
        {
            _sessionService.Delete(id);

            return Ok();
        }
    }
}
