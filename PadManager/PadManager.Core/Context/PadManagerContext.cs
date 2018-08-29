using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using PadManager.Core.Models;

namespace PadManager.Core
{
    public class PandaManagerContext : DbContext
    {
        public virtual DbSet<Pad> Pads { get; set; }
        public PandaManagerContext(DbContextOptions<PandaManagerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Pad>().HasIndex(e => e.Identifier).IsUnique();
            builder.Entity<Pad>().HasMany<Node>(e => e.Nodes).WithOne(e => e.Pad);
            builder.Entity<Pad>().HasMany<InstanceMapping>(e => e.InstanceMappings).WithOne(e => e.Pad);

            builder.Entity<InstanceMapping>().HasIndex(e => e.Identifier).IsUnique();
            builder.Entity<Node>().HasIndex(e => e.Identifier).IsUnique();

        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
             .Where(e => e.State == EntityState.Added ||
               e.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedBy").CurrentValue = Thread.CurrentPrincipal.Identity.Name;
                    entry.Property("LastUpdatedDate").CurrentValue = DateTimeOffset.Now;
                }
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedBy").CurrentValue = Thread.CurrentPrincipal.Identity.Name;
                    entry.Property("CreatedDate").CurrentValue = DateTimeOffset.Now;
                    entry.Property("Identifier").CurrentValue = Guid.NewGuid();
                }
            }
            return base.SaveChanges();
        }
    }
}