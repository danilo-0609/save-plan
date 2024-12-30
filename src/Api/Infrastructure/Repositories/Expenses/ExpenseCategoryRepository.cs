using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Infrastructure.Repositories.Expenses;

public sealed class ExpenseCategoryRepository : IExpenseCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ExpenseCategoryRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public async Task AddAsync(ExpenseCategory expenseCategory)
    {
        await _dbContext.AddAsync(expenseCategory);
    }
}
