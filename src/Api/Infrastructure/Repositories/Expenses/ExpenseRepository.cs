using Microsoft.EntityFrameworkCore;
using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Infrastructure.Repositories.Expenses;

public sealed class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ExpenseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext
            .Expenses
            .Where(r => r.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public void Insert(Expense expense)
    {
        _dbContext.Expenses.Add(expense);
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }
}
