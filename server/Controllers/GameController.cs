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
        }

        [HttpGet]
        public IEnumerable<GameDto> Get()
        {
            var games = _gameService.GetAll();

            var models = games.Select(g => _mapper.Map<GameDto>(g));
            
            return models;
        }

        [HttpPost("[action]")]
        public IActionResult Post([FromBody]GameDto model)
        {
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }

            var game = _mapper.Map<Game>(model);

            _gameService.Update(game);

            return Ok(model);
        }

        [HttpPost("[action]")]
        public IActionResult Delete(Guid id)
        {
            _gameService.Delete(id);

            return Ok();
        }
    }
}