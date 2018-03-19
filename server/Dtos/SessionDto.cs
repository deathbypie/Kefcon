using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Dtos
{
    public class SessionDto : BaseDto
    {
        public TimeslotDto Time { get; set; }

        public GameDto Game { get; set; }

        public IEnumerable<UserDto> Players { get; set; }

        public UserDto GameMaster { get; set; }
    }
}
