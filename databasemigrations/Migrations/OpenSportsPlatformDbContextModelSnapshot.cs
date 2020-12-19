﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using OpenSportsPlatform.Lib.Database;

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    [DbContext(typeof(OpenSportsPlatformDbContext))]
    partial class OpenSportsPlatformDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.Sample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<float?>("AltitudeInMeters")
                        .HasColumnType("real");

                    b.Property<float?>("DistanceInKm")
                        .HasColumnType("real");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Point>("Location")
                        .HasColumnType("geography");

                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<float?>("SpeedKmh")
                        .HasColumnType("real");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SegmentId");

                    b.ToTable("OSPSample");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.Segment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("OSPSegment");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.SportsCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OSPSportcCategory");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

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

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<float?>("HeartRateAvgBpm")
                        .HasColumnType("real");

                    b.Property<float?>("HeartRateMaxBpm")
                        .HasColumnType("real");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("SpeedAvgKmh")
                        .HasColumnType("real");

                    b.Property<float?>("SpeedMaxKmh")
                        .HasColumnType("real");

                    b.Property<int>("SportsCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SportsCategoryId");

                    b.ToTable("OSPWorkout");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.Sample", b =>
                {
                    b.HasOne("OpenSportsPlatform.Lib.Entities.Segment", "Segment")
                        .WithMany("Samples")
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.Segment", b =>
                {
                    b.HasOne("OpenSportsPlatform.Lib.Entities.Workout", "Workout")
                        .WithMany("Segments")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.Workout", b =>
                {
                    b.HasOne("OpenSportsPlatform.Lib.Entities.SportsCategory", "SportsCategory")
                        .WithMany("Workouts")
                        .HasForeignKey("SportsCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SportsCategory");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.Segment", b =>
                {
                    b.Navigation("Samples");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.SportsCategory", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("OpenSportsPlatform.Lib.Entities.Workout", b =>
                {
                    b.Navigation("Segments");
                });
#pragma warning restore 612, 618
        }
    }
}
