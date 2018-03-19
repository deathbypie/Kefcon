using Kefcon.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class Session : BaseEntity
    {
        public Session()
            => Users = new JoinCollection<ApplicationUser, UserSession>(
                UserSessions,
                us => us.User,
                u => new UserSession { Session = this, User = u });

        public Timeslot Time { get; set; }

        public Game Game { get; set; }

        public ApplicationUser GameMaster { get; set; }

        private ICollection<UserSession> UserSessions { get; } = new List<UserSession>();
        [NotMapped]
        public ICollection<ApplicationUser> Users { get; }
    }
}
