using Microsoft.EntityFrameworkCore;
using SavePlan.API.Domain.Expenses;
using SavePlan.API.Domain.Incomes;
using SavePlan.API.Domain.SavingGoals;

namespace SavePlan.API.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Income> Incomes { get; set; }

    public DbSet<IncomeSource> IncomeSources { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public DbSet<SavingGoal> SavingGoals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
