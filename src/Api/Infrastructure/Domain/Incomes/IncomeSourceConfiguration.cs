using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SavePlan.API.Domain.Incomes;

namespace SavePlan.API.Infrastructure.Domain.Incomes;

public sealed class IncomeSourceConfiguration : IEntityTypeConfiguration<IncomeSource>
{
    public void Configure(EntityTypeBuilder<IncomeSource> builder)
    {
        builder.ToTable("IncomeSources", "finances");
    
        builder.HasKey(x => x.Id);

        builder.Property(r => r.Id)
            .HasColumnName("IncomeSourceId");

        builder.Property(r => r.Name);

        builder.Property(g => g.Description);

        builder.Property(r => r.CreatedOn);

        builder.Property(f => f.UpdatedOn)
            .IsRequired(false);


        builder.HasIndex(sg => sg.UserId)
          .HasDatabaseName("IX_IncomeSources_UserId");
    }
}
