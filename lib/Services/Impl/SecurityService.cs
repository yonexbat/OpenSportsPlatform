using OpenSportsPlatform.Lib.Model;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class SecurityService : ISecurityService
    {
        private readonly IPrincipal _principal;
        public SecurityService(IPrincipal principal)
        {
            _principal = principal;
        }

        public string GetCurrentUserid()
        {
            return _principal.Identity.Name;
        }

        public bool IsUserInAnyRole(params Role[] roles)
        {
            //todo: implement
            return false;
        }
    }
}
