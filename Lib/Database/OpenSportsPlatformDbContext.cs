using Microsoft.EntityFrameworkCore;
using OpenSportsPlatform.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenSportsPlatform.Lib.Database
{
    public class OpenSportsPlatformDbContext : DbContext
    {
        public DbSet<Workout> Workout { get; set; }

        public OpenSportsPlatformDbContext(DbContextOptions<OpenSportsPlatformDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

