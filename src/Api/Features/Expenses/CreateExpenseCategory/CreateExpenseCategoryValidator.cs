using FluentValidation;

namespace SavePlan.API.Features.Expenses.CreateExpenseCategory;

public sealed class CreateExpenseCategoryValidator : AbstractValidator<CreateExpenseCategoryCommand>
{
    public CreateExpenseCategoryValidator()
    {
        RuleFor(r => r.Name)
             .NotEmpty()
             .NotNull();

        RuleFor(r => r.Description)
            .NotEmpty()
            .NotNull();
    }
}
