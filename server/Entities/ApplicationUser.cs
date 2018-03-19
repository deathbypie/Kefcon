using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Kefcon.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string UserName { get
            {
                return Email;
            } }

        public IEnumerable<Event> RegisteredEvents { get; set; }
        public IEnumerable<Session> RegisteredSessions { get; set; }
    }
}
