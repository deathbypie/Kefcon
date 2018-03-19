using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class Session : BaseEntity
    {
        public Timeslot Time { get; set; }

        public Game Game { get; set; }
    }
}
