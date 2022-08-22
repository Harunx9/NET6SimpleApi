using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SimpleApi.Model.Base;

public class EFContext : DbContext
{
    private readonly ILoggerFactory? _loggerFactory;
    private readonly ITime? _time;

    public EFContext(DbContextOptions<EFContext> options) : base(options)
    {
    }

    public EFContext(ITime time, ILoggerFactory loggerFactory, DbContextOptions<EFContext> options) : base(options)
    {
        _time = time;
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Debugger.IsAttached && _loggerFactory != null)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskEntity).Assembly);
    }

    public void ResetContextState() => ChangeTracker
        .Entries()
        .ToList()
        .ForEach(x => x?.Reload());

    public override int SaveChanges()
    {
        InternalSaveChanges();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        InternalSaveChanges();

        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        InternalSaveChanges();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void InternalSaveChanges()
    {
        if (_time is null)
        {
            return;
        }

        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            var now = _time.NowUtc();
            if (entry.Entity is IVersionedEntity == false)
            {
                continue;
            }

            var entity = entry.Entity as IVersionedEntity;

            if (entity == null)
            {
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Modified:
                    entity.ModifiedAt = now;
                    entity.Version++;
                    break;
                case EntityState.Added:
                    entity.CreatedAt = now;
                    entity.ModifiedAt = now;
                    break;
            }
        }
    }
}