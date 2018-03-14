using Kefcon.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Services.IdentityServices
{
    public class ApplicationRoleStore : RoleStore<IdentityRole<Guid>, ApplicationDataContext, Guid, IdentityUserRole<Guid>, IdentityRoleClaim<Guid>>
    {
        public ApplicationRoleStore(
            ApplicationDataContext context, 
            IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
