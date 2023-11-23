using OpenSportsPlatform.Lib.Model;
using System.Linq;
using System.Security.Principal;

namespace unittests.util;

public class MockPrincipal
{
    public static IPrincipal CreatePrincipal(string name = "testuser", params Role[] roles)
    {
        GenericIdentity identity = new GenericIdentity(name);
        string[] rolenames = roles.Select(x => x.ToString()).ToArray();
        return new GenericPrincipal(identity, rolenames);
    }
}