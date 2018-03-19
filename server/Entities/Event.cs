using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class Event : BaseEntity
    {
        public ICollection<Timeslot> Timeslots { get; set; }
    }
}
