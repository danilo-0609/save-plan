namespace SavePlan.API.Features.UserInfo;

public sealed record UserInfoResponse(
    string Id, 
    string UserName, 
    string Email, 
    string[] Roles);
