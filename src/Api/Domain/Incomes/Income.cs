namespace SavePlan.API.Domain.Incomes;

public sealed class Income
{ 
    public Guid Id { get; private set; }

    public Guid IncomeSourceId { get; private set; }

    public IncomeCycle IncomeCycle { get; private set; }

    public decimal Amount { get; private set; }

    public TimeSpan Date { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? UpdatedOn { get; private set; }


    public IncomeSource IncomeSource { get; private set; } //Navigation property


    public static Income Create(Guid incomeSourceId,
        IncomeCycle incomeCycle,
        decimal amount,
        TimeSpan date,
        Guid userId,
        DateTime createdOn)
    {
        return new Income(
            Guid.NewGuid(),
            incomeSourceId,
            incomeCycle,
            amount,
            date,
            userId,
            createdOn
        );
    }

    public Income Update(IncomeCycle incomeCycle,
        decimal amount,
        TimeSpan date,
        DateTime updatedOn)
    {
        return new Income(
            Id,
            IncomeSourceId,
            incomeCycle,
            amount,
            date,
            UserId,
            CreatedOn,
            updatedOn
        );
    }

    private Income(Guid id,
        Guid incomeSourceId,
        IncomeCycle incomeCycle,
        decimal amount,
        TimeSpan date,
        Guid userId,
        DateTime createdOn,
        DateTime? updatedOn = null)
    {
        Id = id;
        IncomeSourceId = incomeSourceId;
        IncomeCycle = incomeCycle;
        Amount = amount;
        Date = date;
        UserId = userId;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }

    private Income() { }
}
