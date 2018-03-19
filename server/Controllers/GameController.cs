using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Kefcon.Dtos;
using Kefcon.Entities;
using Kefcon.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kefcon.Controllers
{
    [Route("api/[controller]")]
    public class GameController : BaseController
    {
        private IGameService _gameService;

        public GameController(IGameService gameService, IMapper mapper) : base(mapper)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IEnumerable<GameDto> GetAll()
        {
            var games = _gameService.GetAll();
            var models = games.Select(g => _mapper.Map<GameDto>(g));
            return models;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var game = _gameService.GetById(id);
            var model = _mapper.Map<GameDto>(game);
            return Ok(model);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]GameDto model)
        {
            model.Id = Guid.NewGuid();
            
            var game = _mapper.Map<Game>(model);

            _gameService.Create(game);

            return Ok(model);
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody]GameDto model)
        {
            if(model == null || model.Id == Guid.Empty)
            {
                return BadRequest("Game not found.");
            }
            var game = _mapper.Map<Game>(model);

            _gameService.Update(game);

            return Ok(model);
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(Guid id)
        {
            _gameService.Delete(id);

            return Ok();
        }
    }
}