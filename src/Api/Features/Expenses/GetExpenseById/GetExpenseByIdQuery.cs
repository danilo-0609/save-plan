using ErrorOr;
using SavePlan.API.Common;
using SavePlan.API.Responses.Expenses;

namespace SavePlan.API.Features.Expenses.GetExpenseById;

public sealed record GetExpenseByIdQuery(Guid Id) : IRequest<ErrorOr<ExpenseResponse>>;
