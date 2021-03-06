﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Dtos
{
    public class EventDto : BaseDto
    {
        public string Name { get; set; }

        public IEnumerable<TimeslotDto> Timeslots { get; set; }
    }
}
