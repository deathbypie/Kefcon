using Kefcon.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class Event : BaseEntity
    {
        public Event()
            => Users = new JoinCollection<ApplicationUser, UserEvent>(
                UserEvents,
                ue => ue.User,
                u => new UserEvent { Event = this, User = u });

        public ICollection<Timeslot> Timeslots { get; set; }

        private ICollection<UserEvent> UserEvents { get; } = new List<UserEvent>();
        [NotMapped]
        public ICollection<ApplicationUser> Users { get; }
    }
}
