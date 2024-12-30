namespace SavePlan.API.Responses.Expenses;

public sealed record ExpenseResponse(Guid Id,
    Guid ExpenseCategoryId,
    decimal Amount,
    TimeSpan Date,
    Guid UserId); 
