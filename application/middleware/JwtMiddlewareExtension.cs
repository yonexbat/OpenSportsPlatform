using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application.middleware
{
    public static class JwtMiddlewareExtension
    {
        public static IApplicationBuilder UseJwtMiddleware(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
