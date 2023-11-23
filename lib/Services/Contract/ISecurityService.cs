using OpenSportsPlatform.Lib.Model;
using OpenSportsPlatform.Lib.Model.Entities;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface ISecurityService
{
    string GetCurrentUserid();

    public bool IsUserInAnyRole(params Role[] roles);

    public void CheckAccess(ISecuredEntity securedEntity);
}