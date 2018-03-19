using Kefcon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Data
{
    public class SeedData
    {
        private readonly ApplicationDataContext _context;

        public SeedData(
            ApplicationDataContext context)
        {
            _context = context;
        }

        public void Run()
        {
            InitializeGameData();
        }

        protected void InitializeGameData()
        {
            var games = _context.Games;
            if (!games.Any())
            {
                var testGames = new List<Game>
                {
                    new Game
                    {
                        Name = "Quantum",
                        NumberOfPlayers = 4,
                        Description = "Dice game",
                        Difficulty = GameDifficulty.Medium
                    },
                    new Game
                    {
                        Name = "Splendor",
                        NumberOfPlayers = 4,
                        Description = "Tile game",
                        Difficulty = GameDifficulty.Easy
                    }
                };
                _context.Games.AddRange(testGames);
                _context.SaveChanges();
            }
        }
    }
}
