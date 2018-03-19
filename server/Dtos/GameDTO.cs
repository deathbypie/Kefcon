using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Dtos
{
    public class GameDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfPlayers { get; set; }
        
        public GameDifficulty Difficulty { get; set; }
    }
}
