using Kefcon.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Data
{
    public class ApplicationDataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // https://blog.oneunicorn.com/2017/09/25/many-to-many-relationships-in-ef-core-2-0-part-2-hiding-as-ienumerable/
            modelBuilder.Entity<UserEvent>()
                .HasKey(m => new { m.UserId, m.EventId });
            modelBuilder.Entity<UserEvent>()
                .HasOne(m => m.User)
                .WithMany("UserEvents");
            modelBuilder.Entity<UserEvent>()
                .HasOne(m => m.Event)
                .WithMany("UserEvents");

            modelBuilder.Entity<UserSession>()
                .HasKey(m => new { m.UserId, m.SessionId });
            modelBuilder.Entity<UserSession>()
                .HasOne(m => m.User)
                .WithMany("UserSessions");
            modelBuilder.Entity<UserSession>()
                .HasOne(m => m.Session)
                .WithMany("UserSessions");
        }
    }
}
