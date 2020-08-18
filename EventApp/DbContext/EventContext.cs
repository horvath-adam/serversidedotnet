using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Context
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<ApiLogEntry> ApiLogEntries { get; set; }

        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasOne(e => e.Place).WithMany(p => p.Events).HasForeignKey(e => e.PlaceIdentity);

            modelBuilder.Entity<EventStaff>().HasKey(es => new { es.EventId, es.OrganizerId });
            modelBuilder.Entity<EventStaff>().HasOne(es => es.Event).WithMany(e => e.Staff).HasForeignKey(es => es.EventId);
            modelBuilder.Entity<EventStaff>().HasOne(es => es.Organizer).WithMany(o => o.Events).HasForeignKey(es => es.OrganizerId);

            modelBuilder.RemoveOneToManyCascadeDeleteConvention();
            base.OnModelCreating(modelBuilder);
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void RemoveOneToManyCascadeDeleteConvention(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
