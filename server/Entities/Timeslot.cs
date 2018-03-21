using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class Timeslot : BaseEntity
    {
        public DateTime StartTime { get; set; }

        public TimeSpan Duration;

        public DateTime EndTime
        {
            get
            {
                return StartTime + Duration;
            }
        }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        public Event Event { get;set; }
    }
}
