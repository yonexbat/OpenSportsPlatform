using OpenSportsPlatform.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace unittests.util
{
    public class MockPrincipal
    {
        public static IPrincipal CreatePrincipal(string name = "testuser", params Role[] roles)
        {
            GenericIdentity identity = new GenericIdentity(name);
            string[] rolenames = roles.Select(x => x.ToString()).ToArray();
            return new GenericPrincipal(identity, rolenames);
        }
    }
}
