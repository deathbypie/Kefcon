using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Dtos
{
    public class TimeslotDto : BaseDto
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

        public IEnumerable<SessionDto> Sessions { get; set; }

        public Guid EventId { get; set; }
    }
}
