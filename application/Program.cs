
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using System.Text;
using OpenSportsPlatform.Lib.DependencyInjection;
using OpenSportsPlatform.Application.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
RegisterServies(builder.Services, builder.Configuration);
var app = builder.Build();
RegisterMiddleware(app, configuration);
app.Run();


void RegisterServies(IServiceCollection services, IConfiguration configuration)
{
    if (configuration.GetValue<bool>("UseLettuceEncrypt"))
    {
        services.AddLettuceEncrypt();
    }

    var secret = configuration.GetValue<string>("jwtSecret");
    var key = Encoding.ASCII.GetBytes(secret);

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   var issuer = builder.Configuration.GetValue<string>("jwtIssuer");

                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = issuer,
                       ValidAudience = issuer,
                       IssuerSigningKey = new SymmetricSecurityKey(key)
                   };

                   options.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                           {
                               context.Response.Headers.Add("Token-Expired", "true");
                           }
                           return Task.CompletedTask;
                       }
                   };
               });

    services.AddControllersWithViews();

    // In production, the Angular files will be served from this directory
    services.AddSpaStaticFiles(configuration =>
    {
        configuration.RootPath = "ClientApp/dist";
    });

    services.AddOpenSportsPlatformServices(builder.Configuration);
    services.AddHttpContextAccessor();

    services.AddScoped<IPrincipal>(
        (IServiceProvider sp) => sp.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? throw new InvalidOperationException("Can NOT provide IPrincipal")
    );
}

void RegisterMiddleware(WebApplication app, IConfiguration configuration)
{
    app.ConfigureExceptionHandler();
    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    if (!app.Environment.IsDevelopment())
    {
        app.UseSpaStaticFiles();
    }

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");
    });
    


    
    app.UseSpa(spa =>
    {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "ClientApp";        

        
        if (app.Environment.IsDevelopment())
        {
            spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            // spa.UseAngularCliServer(npmScript: "start");
        }
    });

}
