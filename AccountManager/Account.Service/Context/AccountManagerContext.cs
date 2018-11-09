using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;

namespace AccountManager.Context
{
  public class AccountManagerContext:DbContext
  {
    public virtual DbSet<Models.Account> Accounts{get;set;}
    public virtual DbSet<Models.AccountPad> AccountPads {get;set;}
    public AccountManagerContext(DbContextOptions<AccountManagerContext> options)
    :base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Models.Account>().HasIndex(e => e.Identifier).IsUnique();
      builder.Entity<Models.AccountPad>().HasIndex(e=>e.UserId).IsUnique();
      builder.Entity<Models.AccountPad>().HasIndex(e=>e.PadId).IsUnique();
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