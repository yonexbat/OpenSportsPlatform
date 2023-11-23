using OpenSportsPlatform.Lib.Model;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System.Security;
using System.Security.Principal;

namespace OpenSportsPlatform.Lib.Services.Impl;

public class SecurityService : ISecurityService
{
    private readonly IPrincipal _principal;

    public SecurityService(IPrincipal principal)
    {
        _principal = principal;
    }


    public void CheckAccess(ISecuredEntity securedEntity)
    {
        if (IsUserInAnyRole(Role.Admin))
        {
            return;
        }

        string userId = securedEntity.OwnerUserId;
        string currentPrincipal = GetCurrentUserid();

        if (userId != currentPrincipal)
        {
            throw new SecurityException($"User {currentPrincipal} not allowed to acces workout from user {userId}");
        }
    }

    public string GetCurrentUserid() => _principal?.Identity?.Name ?? "anonymous";

    public bool IsUserInAnyRole(params Role[] roles)
    {
        foreach(Role role in roles)
        {
            if(_principal.IsInRole(role.ToString()))
            {
                return true;
            }
        }
        return false;
    }
}