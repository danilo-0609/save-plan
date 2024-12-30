using FluentValidation;

namespace SavePlan.API.Features.Expenses.CreateExpense;

public sealed class CreateExpenseValidator : AbstractValidator<CreateExpenseCommand>   
{
    public CreateExpenseValidator()
    {
        
    }
}
