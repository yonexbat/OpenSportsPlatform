using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unittests.util;
using Xunit;

namespace unittests
{
    public class JwtTokenServiceTest
    {
        [Fact]
        public void GenerateJwtTokenTest()
        {
            ILogger<JwtTokenService> logger = new MockLogger<JwtTokenService>();
            IConfiguration mockConfiguration = MockConfiguration.GetConfiguraton(new Dictionary<string, string>() {
                ["jwtSecret"] = "jwtSecret. This text must not be too short. It will not work",
            });
            IJwtTokenService service = new JwtTokenService(logger, mockConfiguration);
            string result = service.GenerateJwtToken("hello@gmail.com");

            string res = service.ValidateJwtToken(result);
            Assert.Equal("hello@gmail.com", res);
        }
    }
}
