using Carter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SavePlan.API.Common;
using SavePlan.API.Identity;
using System.Security.Claims;

namespace SavePlan.API.Features.UserInfo;

public sealed class GetCurrentUserInfoEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/me", async (
                           ClaimsPrincipal claimsPrincipal,
                           [FromServices] IdentityDbContext dbContext) =>
        {
            var userId = claimsPrincipal.Claims.First(r => r.Type == ClaimTypes.NameIdentifier).Value;

            var userData = await dbContext
                .Users
                .Where(r => r.Id == userId.ToString())
                .SingleOrDefaultAsync();

            if (userData is null)
            {
                return Results.NotFound("User not found");
            }

            List<string> userRoles = await dbContext
                .Database
                .SqlQuery<string>(
                    $"""
                    SELECT "Name" FROM identity."AspNetRoles" as r
                    INNER JOIN identity."AspNetUserRoles" ur ON r."Id" = ur."RoleId"
                    WHERE ur."UserId" = '{userId}'
                    """)
                .ToListAsync();
                
            UserInfoResponse response = new(userData.Id, userData.UserName!, userData.Email!, userRoles.ToArray());            

            return Results.Ok(response);            
        })
            .RequireAuthorization();
    }
}
