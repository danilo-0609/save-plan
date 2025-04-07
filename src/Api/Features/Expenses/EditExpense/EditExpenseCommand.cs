using ErrorOr;
using MediatR;
using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Features.Expenses.EditExpense;

public sealed record EditExpenseCommand(Guid Id,
    Guid ExpenseCategoryId,
    decimal Amount,
    TimeSpan Date,
    ExpenseCycle ExpenseCycle) : IRequest<ErrorOr<Guid>>;
