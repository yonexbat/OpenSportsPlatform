using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSportsPlatform.Lib.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Database.Mappings
{
    public class SampleMapping : IEntityTypeConfiguration<Sample>
    {
        public void Configure(EntityTypeBuilder<Sample> builder)
        {
            builder.ToTable("OSPSample");            
            builder.HasOne(samp => samp.Segment)
                .WithMany(seg => seg.Samples)
                .HasForeignKey(samp => samp.SegmentId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.AddTechnicalAttributes();
        }
    }
}
