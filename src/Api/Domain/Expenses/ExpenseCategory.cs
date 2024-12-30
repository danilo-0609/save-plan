namespace SavePlan.API.Domain.Expenses;

public sealed class ExpenseCategory
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public Guid UserId { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? UpdatedOn { get; private set; }


    public ICollection<Expense> Expenses { get; private set; } = new List<Expense>(); // Navigation property

    public static ExpenseCategory Create(string name,
        string description,
        Guid userId,
        DateTime createdOn)
    {
        return new ExpenseCategory(Guid.NewGuid(),
            name,
            description,
            userId,
            createdOn);
    }

    public ExpenseCategory Update(string name,
        string description,
        DateTime updatedOn)
    {
        return new ExpenseCategory(Id,
            name,
            description,
            UserId,
            CreatedOn,
            updatedOn);
    }

    private ExpenseCategory(Guid id,
        string name,
        string description,
        Guid userId,
        DateTime createdOn,
        DateTime? updatedOn = null)
    {
        Id = id;
        Name = name;
        Description = description;
        UserId = userId;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }

    private ExpenseCategory() { }
}
