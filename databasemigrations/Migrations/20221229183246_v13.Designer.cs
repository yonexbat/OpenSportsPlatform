﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using OpenSportsPlatform.Lib.Database;

#nullable disable

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    [DbContext(typeof(OpenSportsPlatformDbContext))]
    [Migration("20221229183246_v13")]
    partial class v13
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Sample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("AltitudeInMeters")
                        .HasColumnType("real");

                    b.Property<float?>("CadenceRpm")
                        .HasColumnType("real");

                    b.Property<float?>("DistanceInKm")
                        .HasColumnType("real");

                    b.Property<float?>("HeartRateBpm")
                        .HasColumnType("real");

                    b.Property<DateTimeOffset?>("InsertDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<Point>("Location")
                        .HasColumnType("geography");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<float?>("SpeedKmh")
                        .HasColumnType("real");

                    b.Property<DateTimeOffset?>("Timestamp")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("UpdateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SegmentId");

                    b.ToTable("OSPSample", (string)null);
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Segment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("InsertDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("OSPSegment", (string)null);
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.SportsCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("InsertDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OSPSportcCategory", (string)null);
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("InsertDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OSPTag", (string)null);
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.TagWorkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("InsertDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("OSPTagWorkout", (string)null);
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("InsertDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolarAccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("PolarAccessTokenValidUntil")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PolarUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OSPUserProfile", (string)null);
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("AltitudeMaxInMeters")
                        .HasColumnType("real");

                    b.Property<float?>("AltitudeMinInMeters")
                        .HasColumnType("real");

                    b.Property<float?>("AscendInMeters")
                        .HasColumnType("real");

                    b.Property<float?>("CadenceAvgRpm")
                        .HasColumnType("real");

                    b.Property<float?>("CadenceMaxRpm")
                        .HasColumnType("real");

                    b.Property<float?>("CaloriesInKCal")
                        .HasColumnType("real");

                    b.Property<float?>("DescendInMeters")
                        .HasColumnType("real");

                    b.Property<float?>("DistanceInKm")
                        .HasColumnType("real");

                    b.Property<float?>("DurationInSec")
                        .HasColumnType("real");

                    b.Property<DateTimeOffset?>("EndTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<float?>("HeartRateAvgBpm")
                        .HasColumnType("real");

                    b.Property<float?>("HeartRateMaxBpm")
                        .HasColumnType("real");

                    b.Property<DateTimeOffset?>("InsertDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("SpeedAvgKmh")
                        .HasColumnType("real");

                    b.Property<float?>("SpeedMaxKmh")
                        .HasColumnType("real");

                    b.Property<int>("SportsCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("StartTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("UpdateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdateUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SportsCategoryId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("OSPWorkout", (string)null);
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Sample", b =>
                {
                    b.HasOne("OpenSportsPlatform.Lib.Model.Entities.Segment", "Segment")
                        .WithMany("Samples")
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Segment", b =>
                {
                    b.HasOne("OpenSportsPlatform.Lib.Model.Entities.Workout", "Workout")
                        .WithMany("Segments")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.TagWorkout", b =>
                {
                    b.HasOne("OpenSportsPlatform.Lib.Model.Entities.Tag", "Tag")
                        .WithMany("TagWorkouts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OpenSportsPlatform.Lib.Model.Entities.Workout", "Workout")
                        .WithMany("TagWorkouts")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Workout", b =>
                {
                    b.HasOne("OpenSportsPlatform.Lib.Model.Entities.SportsCategory", "SportsCategory")
                        .WithMany("Workouts")
                        .HasForeignKey("SportsCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OpenSportsPlatform.Lib.Model.Entities.UserProfile", "UserProfile")
                        .WithMany("Workouts")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SportsCategory");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Segment", b =>
                {
                    b.Navigation("Samples");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.SportsCategory", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Tag", b =>
                {
                    b.Navigation("TagWorkouts");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.UserProfile", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Model.Entities.Workout", b =>
                {
                    b.Navigation("Segments");

                    b.Navigation("TagWorkouts");
                });
#pragma warning restore 612, 618
        }
    }
}
