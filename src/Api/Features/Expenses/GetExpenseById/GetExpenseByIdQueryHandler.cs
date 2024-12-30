using ErrorOr;
using SavePlan.API.Common;
using SavePlan.API.Domain.Expenses;
using SavePlan.API.Responses.Expenses;

namespace SavePlan.API.Features.Expenses.GetExpenseById;

public sealed class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ErrorOr<ExpenseResponse>>
{
    private readonly IExpenseRepository _expenseRepository;

    public GetExpenseByIdQueryHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<ErrorOr<ExpenseResponse>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (expense is null)
        {
            return ExpenseErrorCodes.NotFound;
        }

        return new ExpenseResponse(expense.Id, 
            expense.ExpenseCategoryId, 
            expense.Amount, 
            expense.Date,
            expense.UserId);
    }
}
