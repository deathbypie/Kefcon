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
    public class TimeslotController : BaseController
    {
        private ITimeslotService _timeslotService;

        public TimeslotController(IMapper mapper, ITimeslotService timeslotService) : base(mapper)
        {
            _timeslotService = timeslotService;
        }
        [HttpGet]
        public IEnumerable<TimeslotDto> GetAll(Guid eventId)
        {
            var timeslots = _timeslotService.GetAll();
            var timeslotDtos = timeslots.Select(g => _mapper.Map<TimeslotDto>(g));
            return timeslotDtos;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var timeslotEntity = _timeslotService.GetById(id);
            var timeslotDto = _mapper.Map<TimeslotDto>(timeslotEntity);
            return Ok(timeslotDto);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]TimeslotDto timeslotDto)
        {
            timeslotDto.Id = Guid.NewGuid();

            var timeslotEntity = _mapper.Map<Timeslot>(timeslotDto);

            _timeslotService.Create(timeslotEntity, timeslotDto.EventId);

            return Ok(timeslotDto);
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody]TimeslotDto timeslotDto)
        {
            if (timeslotDto == null || timeslotDto.Id == Guid.Empty)
            {
                return BadRequest("Timeslot not found.");
            }
            var timeslotEntity = _mapper.Map<Timeslot>(timeslotDto);

            _timeslotService.Update(timeslotEntity);

            return Ok(timeslotDto);
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(Guid id)
        {
            _timeslotService.Delete(id);

            return Ok();
        }
    }
}
