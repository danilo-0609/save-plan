namespace SavePlan.API.Domain.Incomes;

public sealed class IncomeSource
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public Guid UserId { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? UpdatedOn { get; private set; }

    public ICollection<Income> Incomes { get; private set; } = new List<Income>(); //Navigation propertys

    public static IncomeSource Create(string name,
        string description,
        Guid userId,
        DateTime createdOn)
    {
        return new IncomeSource(Guid.NewGuid(),
            name,
            description,
            userId,
            createdOn);
    }

    public IncomeSource Update(string name,
        string description,
        DateTime updatedOn)
    {
        return new IncomeSource(Id,
            name,
            description,
            UserId,
            CreatedOn,
            updatedOn);
    }

    private IncomeSource(Guid id,
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

    private IncomeSource() { }

}
