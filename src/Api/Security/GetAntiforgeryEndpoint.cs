using Carter;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;

namespace SavePlan.API.Security;

public sealed class GetAntiforgeryEndpoint : CarterModule
{
    public GetAntiforgeryEndpoint()
        : base("antiforgery")
    {
        
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("token", (IAntiforgery forgeryService, HttpContext context) =>
        {
            var tokens = forgeryService.GetAndStoreTokens(context);
            context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!,
                    new CookieOptions { HttpOnly = false });

            return Results.Ok();
        })
            .RequireAuthorization();
    }
}
