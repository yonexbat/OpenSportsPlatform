using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSportsPlatform.Lib.Model.Entities;

namespace OpenSportsPlatform.Lib.Database.Mappings;

public class WorkoutMapping : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.ToTable("OSPWorkout");

        builder.HasOne(wo => wo.SportsCategory)
            .WithMany(cat => cat.Workouts)
            .HasForeignKey(c => c.SportsCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(wo => wo.UserProfile)
            .WithMany(cat => cat.Workouts)
            .HasForeignKey(c => c.UserProfileId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.AddTechnicalAttributes();
    }
}