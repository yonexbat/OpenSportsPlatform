using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class SecurityService : ISecurityService
    {
        public string GetCurrentUserid()
        {
            return "anonymous";
        }
    }
}
