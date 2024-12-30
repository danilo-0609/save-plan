
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace SavePlan.API.Common;

public sealed class ExecutionContextAccessor : IExecutionContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        if (_httpContextAccessor.HttpContext is not null &&
                _httpContextAccessor.HttpContext.User is not null &&
                _httpContextAccessor.HttpContext.User.Claims is not null)
        {
            string? subClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (subClaim is null)
            {
                throw new Exception("User id was not found");
            }

            Match match = Regex.Match(subClaim, @"\b([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})\b");

            if (Guid.TryParse(match.Value, out var userId))
            {
                return userId;
            }
        }

        throw new Exception("User id was not found");
    }
}
