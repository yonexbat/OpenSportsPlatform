﻿using Microsoft.EntityFrameworkCore;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System.Reflection;

namespace OpenSportsPlatform.Lib.Database;

public class OpenSportsPlatformDbContext : DbContext
{
    private readonly ISecurityService _securityService;

    public DbSet<Workout> Workout { get; set; }

    public DbSet<SportsCategory> SportsCategory { get; set; }

    public DbSet<UserProfile> UserProfile { get; set; }

    public DbSet<Sample> Sample { get; set; }

    public DbSet<Tag> Tag { get; set; }

    public DbSet<TagWorkout> TagWorkout { get; set; }
    
    public DbSet<Segment> Segment { get; set; }

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