﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSportsPlatform.Lib.Model.Entities;

namespace OpenSportsPlatform.Lib.Database.Mappings;

public class UserProfileMapping : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("OSPUserProfile");
        builder.AddTechnicalAttributes();
    }
}