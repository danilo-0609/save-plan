namespace SavePlan.API.Requests.Expenses;

public sealed record CreateExpenseRequest(Guid ExpenseCategoryId,
    decimal Amount,
    TimeSpan Date,
    int ExpenseCycle);
