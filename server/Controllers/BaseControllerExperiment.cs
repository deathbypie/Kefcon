using AutoMapper;
using Kefcon.Data;
using Kefcon.Dtos;
using Kefcon.Entities;
using Kefcon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Controllers
{
    [Authorize]
    public abstract class BaseControllerExperiment<Entity, Dto> : Controller
        where Entity : BaseEntity
        where Dto : BaseDto
    {
        protected readonly IMapper _mapper;
        protected readonly IServiceBase<Entity> _entityService;

        public BaseControllerExperiment(IMapper mapper, IServiceBase<Entity> entityService)
        {
            _mapper = mapper;
            _entityService = entityService;
        }

        [HttpGet]
        public IEnumerable<Dto> GetAll()
        {
            var entities = _entityService.GetAll();
            var dtos = entities.Select(g => _mapper.Map<Dto>(g));
            return dtos;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var entity = _entityService.GetById(id);
            var dto = _mapper.Map<Dto>(entity);
            return Ok(dto);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]Dto dto)
        {
            dto.Id = Guid.NewGuid();

            var entity = _mapper.Map<Entity>(dto);

            _entityService.Create(entity);

            return Ok(dto);
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody]Dto dto)
        {
            if (dto == null || dto.Id == Guid.Empty)
            {
                return BadRequest("Entity not found.");
            }
            var entity = _mapper.Map<Entity>(dto);

            _entityService.Update(entity);

            return Ok(dto);
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(Guid id)
        {
            _entityService.Delete(id);

            return Ok();
        }
    }
}
