using ErrorOr;
using SavePlan.API.Common;
using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Features.Expenses.CreateExpense;

public sealed class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ErrorOr<Guid>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IExecutionContextAccessor _contextAccessor;

    public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, IExecutionContextAccessor contextAccessor)
    {
        _expenseRepository = expenseRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var userId = _contextAccessor.GetUserId();

        var expense = Expense.Create(request.ExpenseCategoryId,
            request.Amount,
            request.Date,
            (ExpenseCycle)request.ExpenseCycle,
            userId,
            DateTime.Now);
    
        await _expenseRepository.InsertAsync(expense);

        return expense.Id;
    }
}
