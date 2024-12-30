namespace SavePlan.API.Domain.Expenses;

public interface IExpenseRepository
{
    Task InsertAsync(Expense expense);  

    Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
