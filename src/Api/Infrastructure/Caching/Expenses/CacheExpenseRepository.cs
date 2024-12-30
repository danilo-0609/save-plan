using Microsoft.Extensions.Caching.Memory;
using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Infrastructure.Caching.Expenses;

public sealed class CacheExpenseRepository : IExpenseRepository
{
    private readonly IExpenseRepository _decorated;
    private readonly IMemoryCache _cache;
    private readonly ApplicationDbContext _dbContext;

    public CacheExpenseRepository(IExpenseRepository decorated, IMemoryCache cache, ApplicationDbContext dbContext)
    {
        _decorated = decorated;
        _cache = cache;
        _dbContext = dbContext;
    }

    public async Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        string key = $"expense-{id}";

        return await _cache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return _decorated.GetByIdAsync(id, cancellationToken);
        });
    }

    public async Task InsertAsync(Expense expense)
    {
        await _decorated.InsertAsync(expense);
    }
}
