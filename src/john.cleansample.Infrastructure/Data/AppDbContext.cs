using System.Reflection;
using Ardalis.SharedKernel;
using EFCore.UnitOfWorkCore.Interfaces;
using john.cleansample.Core.ContributorAggregate;
using john.cleansample.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace john.cleansample.Infrastructure.Data;
public class AppDbContext : DbContext, IJayDbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }

  public DbSet<Contributor> Contributors => Set<Contributor>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    //Use the bellow line to apply configuration for each entity
    //modelBuilder.ApplyConfiguration(new ContributorConfiguration());

    //Use the bellow line to apply configuration for all entities
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
