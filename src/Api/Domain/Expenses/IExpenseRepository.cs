namespace SavePlan.API.Domain.Expenses;

public interface IExpenseRepository
{
    void Insert(Expense expense);  

    Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    void Update(Expense expense);   
}
