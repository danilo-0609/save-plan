using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SavePlan.API.Common;
using SavePlan.API.Requests.Expenses;

namespace SavePlan.API.Features.Expenses.CreateExpenseCategory;

public sealed class CreateExpenseCategoryEndpoint : CarterModule
{
    public CreateExpenseCategoryEndpoint()
        : base(EndpointBaseNames.Categories)
    {
        
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async ([FromBody] CreateExpenseCategoryRequest request, 
            [FromServices] ISender sender) =>
        {
            var command = new CreateExpenseCategoryCommand(request.Name, request.Description);

            var result = await sender.Send(command);

            return result.Match(
                onValue => Results.Created($"api/{EndpointBaseNames.Categories}/{result.Value}", result.Value),
                onError => Problem.Errors(result.Errors));
        });
    }
}
