using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSportsPlatform.Lib.Model.Entities;

namespace OpenSportsPlatform.Lib.Database.Mappings;

public class SportsCategoryMapping : IEntityTypeConfiguration<SportsCategory>
{
    public void Configure(EntityTypeBuilder<SportsCategory> builder)
    {
        builder.ToTable("OSPSportcCategory");
        builder.Property(x => x.Name).IsRequired(false);

        builder.AddTechnicalAttributes();
    }
}