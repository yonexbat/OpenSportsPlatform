using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSportsPlatform.Lib.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Database.Mappings
{
    public class SegmentMapping : IEntityTypeConfiguration<Segment>
    {
        public void Configure(EntityTypeBuilder<Segment> builder)
        {
            builder.ToTable("OSPSegment");
            builder.HasOne(seg => seg.Workout)
                .WithMany(wo => wo.Segments)
                .HasForeignKey(seg => seg.WorkoutId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.AddTechnicalAttributes();
        }
    }
}
