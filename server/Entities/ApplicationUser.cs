using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Kefcon.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Kefcon.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser() : base()
        {
            Events = new JoinCollection<Event, UserEvent>(
                UserEvents,
                ue => ue.Event,
                e => new UserEvent { User = this, Event = e });

            Sessions = new JoinCollection<Session, UserSession>(
                UserSessions,
                us => us.Session,
                s => new UserSession { User = this, Session = s });
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string UserName { get
            {
                return Email;
            } }

        private ICollection<UserEvent> UserEvents { get; } = new List<UserEvent>();
        [NotMapped]
        public ICollection<Event> Events { get; }

        private ICollection<UserSession> UserSessions { get; } = new List<UserSession>();
        [NotMapped]
        public ICollection<Session> Sessions { get; }

    }
}
