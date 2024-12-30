using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SavePlan.API.Common;
using SavePlan.API.Domain.Expenses;
using SavePlan.API.Domain.Incomes;
using SavePlan.API.Domain.SavingGoals;
using SavePlan.API.Infrastructure.Caching.Expenses;
using SavePlan.API.Infrastructure.Repositories.Expenses;
using SavePlan.API.Infrastructure.Repositories.Incomes;
using SavePlan.API.Infrastructure.Repositories.SavingGoals;

namespace SavePlan.API.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, optionsBuilder) =>
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Database"));
        });

        services.AddMemoryCache();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.Decorate<IExpenseRepository, CacheExpenseRepository>();
        
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<IIncomeSourceRepository, IncomeSourceRepository>();
        services.AddScoped<ISavingGoalRepository, SavingGoalRepository>();

        return services;
    }
}
