using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OpenSportsPlatform.DatabaseMigrations;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Services.Contract;
using OpenSportsPlatform.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.DatabaseMigrations.OSPMigration
{
    public class DbContextFactoryForMigrations : IDesignTimeDbContextFactory<OpenSportsPlatformDbContext>
    {
        public OpenSportsPlatformDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .AddUserSecrets<Program>()
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<OpenSportsPlatformDbContext>();
            optionsBuilder.UseSqlServer(connectionString, 
                    b => 
                        b.MigrationsAssembly("OpenSportsPlatform.DatabaseMigrations")                        
                        .UseNetTopologySuite()
                        .CommandTimeout(100000)
                );

            GenericIdentity identity = new GenericIdentity("technicaluser");
            GenericPrincipal principal = new GenericPrincipal(identity, new string[0]);

            ISecurityService securityService = new SecurityService(principal);

            return new OpenSportsPlatformDbContext(optionsBuilder.Options, securityService);
        }
    }
}
