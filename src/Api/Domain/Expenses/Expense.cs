namespace SavePlan.API.Domain.Expenses;

//TODO: Añadir evento de dominio para llevar a análisis con IA.
public sealed class Expense
{
    public Guid Id { get; private set; }

    public Guid ExpenseCategoryId { get; private set; }

    public decimal Amount { get; private set; }

    public TimeSpan Date { get; private set; }

    public ExpenseCycle ExpenseCycle { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? UpdatedOn { get; private set; }

    public ExpenseCategory ExpenseCategory { get; private set; } // Navigation property

    public static Expense Create(Guid categoryId,
        decimal amount,
        TimeSpan date,
        ExpenseCycle expenseCycle,
        Guid userId,
        DateTime createdOn)
    {
        return new Expense(Guid.NewGuid(),
            categoryId,
            amount,
            date,
            expenseCycle,
            userId,
            createdOn);
    }

    public static Expense Update(Guid id,
        Guid categoryId,
        decimal amount,
        TimeSpan date,
        ExpenseCycle expenseCycle,
        Guid userId,
        DateTime createdOn,
        DateTime updatedOn)
    {
        return new Expense(id,
            categoryId,
            amount,
            date,
            expenseCycle,
            userId,
            createdOn,
            updatedOn);
    }


    private Expense(Guid id,
        Guid categoryId,
        decimal amount,
        TimeSpan date,
        ExpenseCycle expenseCycle,
        Guid userId,
        DateTime createdOn,
        DateTime? updatedOn = null)
    {
        Id = id;
        ExpenseCategoryId = categoryId;
        Amount = amount;
        Date = date;
        ExpenseCycle = expenseCycle;
        UserId = userId;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }

    private Expense() { }
}
