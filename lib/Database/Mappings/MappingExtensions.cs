using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSportsPlatform.Lib.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenSportsPlatform.Lib.Database.Mappings
{
    internal static class MappingExtensions
    {
        public static void AddTechnicalAttributes<T>(this EntityTypeBuilder<T> builder) where T : class, IEntity
        {
            builder.Property(x => x.InsertUser)
                .IsRequired();
            builder.Property(x => x.InsertDate)
               .IsRequired()
               .HasDefaultValueSql("GetUtcDate()");
            builder.Property(x => x.UpdateUser)
                 .IsRequired();
            builder.Property(x => x.UpdateDate)
                 .IsRequired()
                 .HasDefaultValueSql("GetUtcDate()");
        }

    }
}
