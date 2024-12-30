using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SavePlan.API.Domain.Incomes;

namespace SavePlan.API.Infrastructure.Domain.Incomes;

public sealed class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("Incomes", "finances");

        builder.HasKey(r => r.Id);

        builder.Property(t => t.Id)
            .HasColumnName("IncomeId");

        builder.Property(t => t.IncomeSourceId);

        builder.Property(t => t.Amount)
            .HasColumnType("decimal(18,2)"); 

        builder.Property(t => t.Date);

        builder.Property(t => t.UserId);

        builder.Property(t => t.CreatedOn);

        builder.Property(t => t.UpdatedOn)
            .IsRequired(false);

        builder.Property(t => t.IncomeCycle);
    
        builder.HasOne(r => r.IncomeSource)
            .WithMany(g => g.Incomes)
            .HasForeignKey(h => h.IncomeSourceId)
            .OnDelete(DeleteBehavior.Cascade); //TODO: Does not need to be cascade either... 


        builder.HasIndex(sg => sg.UserId)
                  .HasDatabaseName("IX_Incomes_UserId");
    }
}
