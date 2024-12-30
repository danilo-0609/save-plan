using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SavePlan.API.Common;
using SavePlan.API.Requests.Expenses;

namespace SavePlan.API.Features.Expenses.GetExpenseById;

public sealed class GetExpenseEndpoint : CarterModule
{
    private IHttpContextAccessor _httpContextAccessor;

    public GetExpenseEndpoint(IHttpContextAccessor contextAccessor)
        : base(EndpointBaseNames.Expenses)
    {
        _httpContextAccessor = contextAccessor;
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (Guid id,
             [FromServices] ISender sender) =>
        {
            var query = new GetExpenseByIdQuery(id);

            var result = await sender.Send(query);

            if (result.IsError)
            {
                return Problem.Errors(result.Errors);
            }

            return Results.Ok(result.Value);
        })
            .RequireAuthorization();
    }
}
