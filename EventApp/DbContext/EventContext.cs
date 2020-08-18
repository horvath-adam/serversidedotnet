using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Context
{
    public class EventContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<ApiLogEntry> ApiLogEntries { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasOne(e => e.Place).WithMany(p => p.Events).HasForeignKey(e => e.PlaceIdentity);

            modelBuilder.Entity<EventStaff>().HasKey(es => new { es.EventId, es.OrganizerId });
            modelBuilder.Entity<EventStaff>().HasOne(es => es.Event).WithMany(e => e.Staff).HasForeignKey(es => es.EventId);
            modelBuilder.Entity<EventStaff>().HasOne(es => es.Organizer).WithMany(o => o.Events).HasForeignKey(es => es.OrganizerId);

            modelBuilder.Entity<ApplicationUser>().HasKey(appuser => appuser.Id);
            //modelBuilder.Entity<IdentityUserRole<int>>()
            //    .ToTable("AspNetUserRoles")
            //    .HasKey(p => new { p.UserId, p.RoleId });
            //modelBuilder.Entity<IdentityRole<int>>()
            //    .ToTable("AspNetRoles")
            //    .HasKey(p => p.Id);
            //modelBuilder.Entity<IdentityUserClaim<int>>()
            //   .ToTable("ApplicationUserClaims")
            //   .HasKey(p => p.Id);


            modelBuilder.RemoveOneToManyCascadeDeleteConvention();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

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
