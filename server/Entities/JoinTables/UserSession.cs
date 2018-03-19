using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class UserSession
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; }
    }
}
