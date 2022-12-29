using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSportsPlatform.Lib.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Database.Mappings
{
    public class TagWorkoutMapping : IEntityTypeConfiguration<TagWorkout>
    {
        public void Configure(EntityTypeBuilder<TagWorkout> builder)
        {
            builder.ToTable("OSPTagWorkout");

            builder.HasOne(tagWorkout => tagWorkout.Workout)
              .WithMany(wo => wo.TagWorkouts)
              .HasForeignKey(tagWorkout => tagWorkout.WorkoutId)
              .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(tagWorkout => tagWorkout.Tag)
                          .WithMany(wo => wo.TagWorkouts)
                          .HasForeignKey(tagWorkout => tagWorkout.TagId)
                          .OnDelete(DeleteBehavior.ClientCascade);

            builder.AddTechnicalAttributes();

        }
    }
}
