using ConsoleApp1.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<OrganizerProfile> organizerProfiles { get; set; }  //optional
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Organizer&OrganizerProfile 1-1
            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.Profile)
                .WithOne(p => p.Organizer)
                .HasForeignKey<OrganizerProfile>(p => p.OrganizerId);

            // Event Self Relationship
            modelBuilder.Entity<Event>()
                .HasOne(e => e.ParentEvent)
                .WithMany(e => e.Sessions)
                .HasForeignKey(e => e.ParentEventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Attendee&Event 1-M
            modelBuilder.Entity<Registration>()
                .HasKey(r => new { r.AttendeeId, r.EventId });//Composite Key

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Attendee)
                .WithMany(a => a.Registrations)
                .HasForeignKey(r => r.AttendeeId);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId);

            // Attendee&Badge 1-1
            modelBuilder.Entity<Attendee>()
                .HasOne(a => a.Badge)
                .WithOne(b => b.Attendee)
                .HasForeignKey<Badge>(b => b.AttendeeId);

            // Owned Type
            modelBuilder.Entity<Attendee>()
                .OwnsOne(a => a.Address);

            // Shadow Properties
            modelBuilder.Entity<Event>()
                .Property<DateTime>("CreatedAt");

            modelBuilder.Entity<Event>()
                .Property<DateTime>("LastModified");
        }
    }
}
