using OpenSportsPlatform.Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface ISecurityService
    {
        string GetCurrentUserid();

        public bool IsUserInAnyRole(params Role[] roles);
    }
}
