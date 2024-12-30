using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SavePlan.API.Domain.Expenses;

namespace SavePlan.API.Infrastructure.Domain.Expenses;

public sealed class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.ToTable("ExpenseCategories", "finances");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("ExpenseCategoryId")
            .ValueGeneratedNever();

        builder.Property(r => r.Name)
            .HasColumnName("Name");

        builder.Property(r => r.Description)
            .HasColumnName("Description")
            .HasDefaultValue(string.Empty);

        builder.Property(r => r.UserId)
            .HasColumnName("UserId");

        builder.Property(r => r.CreatedOn)
            .HasColumnName("CreatedOn");

        builder.Property(r => r.UpdatedOn)
            .HasColumnName("UpdatedOn")
            .IsRequired(false);


        builder.HasIndex(sg => sg.UserId)
          .HasDatabaseName("IX_Expenses_UserId");
    }
}
