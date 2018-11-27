using System;
using System.Linq;
using System.Threading;
using Engine.Enums;
using Microsoft.EntityFrameworkCore;
using PadManager.Core.Models;

namespace PadManager.Core
{
  public class PandaManagerContext : DbContext
  {
    public virtual DbSet<Models.AccountPad> AccountPads { get; set; }

    public virtual DbSet<Pad> Pads { get; set; }

    public virtual DbSet<PadExecutionHistory> PadExecutionHistory { get; set; }
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

      builder.Entity<Models.AccountPad>().ToTable("AccountPad");
      builder.Entity<Models.AccountPad>().HasIndex(e => e.UserId).IsUnique();
      builder.Entity<Models.AccountPad>().HasIndex(e => e.PadId).IsUnique();

      builder.Entity<Models.PadExecutionHistory>().HasIndex(e => e.PadIdentifier);
      builder.Entity<Models.PadExecutionHistory>().HasIndex(e => e.UserId);
      builder.Entity<Models.PadExecutionHistory>().Property(e => e.Status).HasConversion(
        v => v.ToString(),
        v => (ExecutionStatus)Enum.Parse(typeof(ExecutionStatus), v)
      );
    }
    public override int SaveChanges()
    {
      foreach (var entry in ChangeTracker.Entries()
       .Where(e => e.State == EntityState.Added ||
         e.State == EntityState.Modified))
      {
        if (entry.Entity is Models.AccountPad)
          continue;
        if (entry.State == EntityState.Modified)
        {
          entry.Property("Identifier").IsModified = false;
          entry.Property("UpdatedBy").CurrentValue = Thread.CurrentPrincipal?.Identity?.Name ?? "PadManager";
          entry.Property("LastUpdatedDate").CurrentValue = DateTimeOffset.Now;
        }
        if (entry.State == EntityState.Added)
        {
          entry.Property("CreatedBy").CurrentValue = Thread.CurrentPrincipal?.Identity?.Name ?? "PadManager";
          entry.Property("CreatedDate").CurrentValue = DateTimeOffset.Now;
          entry.Property("Identifier").CurrentValue = Guid.NewGuid();
        }
      }
      return base.SaveChanges();
    }
  }
}