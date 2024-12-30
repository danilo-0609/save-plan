using ErrorOr;
using SavePlan.API.Common;
using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Features.Expenses.CreateExpenseCategory;

public sealed class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommand, ErrorOr<Guid>>
{
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;

    public CreateExpenseCategoryCommandHandler(IExecutionContextAccessor executionContextAccessor, IExpenseCategoryRepository expenseCategoryRepository)
    {
        _executionContextAccessor = executionContextAccessor;
        _expenseCategoryRepository = expenseCategoryRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var userId = _executionContextAccessor.GetUserId();

        var expenseCategory = ExpenseCategory.Create(request.Name,
            request.Description,
            userId,
            DateTime.Now);
    
        await _expenseCategoryRepository.AddAsync(expenseCategory);

        return expenseCategory.Id;
    }
}
