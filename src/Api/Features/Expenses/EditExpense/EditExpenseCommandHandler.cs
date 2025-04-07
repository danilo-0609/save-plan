using ErrorOr;
using MediatR;
using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Features.Expenses.EditExpense;

public sealed class EditExpenseCommandHandler : IRequestHandler<EditExpenseCommand, ErrorOr<Guid>>
{
    private readonly IExpenseRepository _expenseRepository;

    public EditExpenseCommandHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(EditExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetByIdAsync(request.Id, cancellationToken);
    
        if (expense is null)
        {
            return ExpenseErrorCodes.NotFound;
        }

        var expenseUpdate = Expense.Update(request.Id,
            request.ExpenseCategoryId,
            request.Amount,
            request.Date,
            request.ExpenseCycle,
            expense.UserId,
            expense.CreatedOn,
            DateTime.Now);

        _expenseRepository.Update(expenseUpdate);

        return expense.Id;
    }
}
