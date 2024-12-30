namespace SavePlan.API.Domain.SavingGoals;

public sealed class SavingGoal
{
    public Guid Id { get; private set; }

    public decimal Amount { get; private set; }

    public string SavingGoalReason { get; private set; } = string.Empty;

    public SavingGoalCycle SavingGoalCycle { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? UpdatedOn { get; private set; }

    public static SavingGoal Create(decimal amount,
        string savingGoalReason,
        SavingGoalCycle savingGoalCycle,
        Guid userId,
        DateTime createdOn)
    {
        return new SavingGoal(
            Guid.NewGuid(),
            amount,
            savingGoalReason,
            savingGoalCycle,
            userId,
            createdOn
        );
    }

    public SavingGoal Update(decimal amount,
        string savingGoalReason,
        SavingGoalCycle savingGoalCycle,
        DateTime updatedOn)
    {
        return new SavingGoal(
            Id,
            amount,
            savingGoalReason,
            savingGoalCycle,
            UserId,
            CreatedOn,
            updatedOn
        );
    }

    private SavingGoal(Guid id,
        decimal amount,
        string savingGoalReason,
        SavingGoalCycle savingGoalCycle,
        Guid userId,
        DateTime createdOn,
        DateTime? updatedOn = null)
    {
        Id = id;
        Amount = amount;
        SavingGoalReason = savingGoalReason;
        SavingGoalCycle = savingGoalCycle;
        UserId = userId;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }
}
