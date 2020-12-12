using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OpenSportsPlatform.DatabaseMigrations;
using OpenSportsPlatform.Lib.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("OpenSportsPlatform.DatabaseMigrations"));

            return new OpenSportsPlatformDbContext(optionsBuilder.Options);
        }
    }
}
