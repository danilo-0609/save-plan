using ErrorOr;
using SavePlan.API.Common;

namespace SavePlan.API.Features.Expenses.CreateExpenseCategory;

public sealed record CreateExpenseCategoryCommand(string Name, string Description): IRequest<ErrorOr<Guid>>;

