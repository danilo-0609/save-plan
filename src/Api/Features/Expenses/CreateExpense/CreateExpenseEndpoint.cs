using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SavePlan.API.Common;
using SavePlan.API.Requests.Expenses;

namespace SavePlan.API.Features.Expenses.CreateExpense;

public sealed class CreateExpenseEndpoint : CarterModule
{
   
    public CreateExpenseEndpoint(IHttpContextAccessor contextAccessor)
        : base(EndpointBaseNames.Expenses)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async(
             [FromBody] CreateExpenseRequest request,
             [FromServices] ISender sender) =>
        {
            var command = new CreateExpenseCommand(request.ExpenseCategoryId,
                request.Amount,
                request.Date,
                request.ExpenseCycle);

            var result = await sender.Send(command);

            return result.Match(
                onValue => Results.Created($"api/{EndpointBaseNames.Expenses}/{result.Value}", result.Value),
                onError => Problem.Errors(result.Errors));            
        });
    }
}
