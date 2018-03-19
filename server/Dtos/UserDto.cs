using System;
using System.Collections.Generic;

namespace Kefcon.Dtos
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<EventDto> RegisteredEvents { get; set; }
    }
}