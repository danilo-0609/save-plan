using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Infrastructure.Domain.Expenses;

public sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses", "finances");

        builder.HasKey(g => g.Id);

        builder.Property(f => f.Id)
            .HasColumnName("ExpenseId");

        builder.Property(e => e.ExpenseCategoryId);

        builder.Property(e => e.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.Date);

        builder.Property(e => e.UserId);

        builder.Property(e => e.CreatedOn);

        builder.Property(e => e.UpdatedOn)
            .IsRequired(false);

        builder.Property(e => e.ExpenseCycle);

        // Relationship configuration
        builder.HasOne(e => e.ExpenseCategory)
            .WithMany(ec => ec.Expenses)
            .HasForeignKey(e => e.ExpenseCategoryId)
            .OnDelete(DeleteBehavior.Cascade); //This does not have to be cascade... 


        builder.HasIndex(sg => sg.UserId)
          .HasDatabaseName("IX_ExpenseCategories_UserId");
    }
}
