using Microsoft.EntityFrameworkCore;
using OpenSportsPlatform.Lib.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenSportsPlatform.Lib.Database
{
    public class OpenSportsPlatformDbContext : DbContext
    {
        private readonly ISecurityService _securityService;
        public DbSet<Workout> Workout { get; set; }

        public DbSet<SportsCategory> SportsCategory { get; set; }

        public OpenSportsPlatformDbContext(
            DbContextOptions<OpenSportsPlatformDbContext> options, 
            ISecurityService securityService) : base(options)
        {
            _securityService = securityService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditDbInterceptor(_securityService));
            base.OnConfiguring(optionsBuilder);
        }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

