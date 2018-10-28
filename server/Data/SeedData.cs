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
            InitializeTestEvent();
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

        protected void InitializeTestEvent()
        {
            var events = _context.Events;
            if (!events.Any())
            {
                var timeSlot1 = new Timeslot
                {
                    StartTime = DateTime.Now.AddDays(2),

                    Duration = new TimeSpan(2, 0, 0), // 2 hours

                    Sessions = new List<Session>
                    {
                        new Session
                        {
                            Game = _context.Games.FirstOrDefault(),
                            GameMaster = _context.Users.FirstOrDefault()
                        },
                        new Session
                        {
                            Game = _context.Games.LastOrDefault(),
                            GameMaster = _context.Users.FirstOrDefault()
                        }
                    }
                };

                var timeSlot2 = new Timeslot
                {
                    StartTime = DateTime.Now.AddDays(2) + new TimeSpan(2,0,0),

                    Duration = new TimeSpan(2, 0, 0), // 2 hours

                    Sessions = new List<Session>
                    {
                        new Session
                        {
                            Game = _context.Games.FirstOrDefault(),
                            GameMaster = _context.Users.FirstOrDefault()
                        },
                        new Session
                        {
                            Game = _context.Games.LastOrDefault(),
                            GameMaster = _context.Users.FirstOrDefault()
                        }
                    }
                };

                var newEvent = new Event
                {
                    Name = "Test Event",
                    Timeslots = new List<Timeslot> { timeSlot1,timeSlot2}
                };

                _context.Events.Add(newEvent);
                _context.SaveChanges();
            }
        }
    }
}
