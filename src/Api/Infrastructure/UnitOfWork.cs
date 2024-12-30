using SavePlan.API.Common;

namespace SavePlan.API.Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //TODO: Implement everything about events to send them here using outbox pattern

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
