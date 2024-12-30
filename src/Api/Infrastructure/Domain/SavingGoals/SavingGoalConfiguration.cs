using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SavePlan.API.Domain.SavingGoals;

namespace SavePlan.API.Infrastructure.Domain.SavingGoals;

public sealed class SavingGoalConfiguration : IEntityTypeConfiguration<SavingGoal>
{
    public void Configure(EntityTypeBuilder<SavingGoal> builder)
    {
        builder.ToTable("SavingGoals", "finances");

        builder.HasKey(sg => sg.Id);

        builder.Property(sg => sg.Id)
               .HasColumnName("SavingGoalId");

        builder.Property(sg => sg.Amount)
               .HasColumnType("decimal(18,2)"); 

        builder.Property(sg => sg.SavingGoalReason);

        builder.Property(sg => sg.SavingGoalCycle);

        builder.Property(sg => sg.UserId);

        builder.Property(sg => sg.CreatedOn);

        builder.Property(sg => sg.UpdatedOn)
            .IsRequired(false);


        builder.HasIndex(sg => sg.UserId)
                  .HasDatabaseName("IX_SavingGoals_UserId");
    }
}
