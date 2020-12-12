using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSportsPlatform.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Database.Mappings
{
    public class WorkoutMapping : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.ToTable("OSPWorkout");

            builder.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .UseIdentityColumn();
           
        }
    }
}
