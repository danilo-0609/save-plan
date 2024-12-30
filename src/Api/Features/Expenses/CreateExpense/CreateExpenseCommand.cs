using ErrorOr;
using SavePlan.API.Common;

namespace SavePlan.API.Features.Expenses.CreateExpense;

public sealed record CreateExpenseCommand(Guid ExpenseCategoryId,
    decimal Amount,
    TimeSpan Date,
    int ExpenseCycle) : IRequest<ErrorOr<Guid>>;

