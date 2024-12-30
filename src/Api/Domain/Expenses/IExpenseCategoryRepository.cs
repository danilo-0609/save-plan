namespace SavePlan.API.Domain.Expenses;

public interface IExpenseCategoryRepository
{
    Task AddAsync(ExpenseCategory expenseCategory);
}
