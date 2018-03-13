using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Entities
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}
