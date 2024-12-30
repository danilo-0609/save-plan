using ErrorOr;

namespace SavePlan.API.Domain.Expenses;

public class ExpenseErrorCodes
{
    public static Error NotFound =>
        Error.NotFound("Expense.NotFound", "The expense was not found");
}
