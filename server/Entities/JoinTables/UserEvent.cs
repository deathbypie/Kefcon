using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class UserEvent
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
