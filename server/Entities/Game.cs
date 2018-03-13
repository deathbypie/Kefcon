using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfPlayers { get; set; }

        public GameDifficulty Difficulty { get; set; }
    }
}
