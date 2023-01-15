using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OSPSegment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSPSegment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OSPSegment_OSPWorkout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "OSPWorkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OSPSample",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    AltitudeInMeters = table.Column<float>(type: "real", nullable: true),
                    DistanceInKm = table.Column<float>(type: "real", nullable: true),
                    SpeedKmh = table.Column<float>(type: "real", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<Point>(type: "geography", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSPSample", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OSPSample_OSPSegment_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "OSPSegment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OSPSample_SegmentId",
                table: "OSPSample",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OSPSegment_WorkoutId",
                table: "OSPSegment",
                column: "WorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OSPSample");

            migrationBuilder.DropTable(
                name: "OSPSegment");
        }
    }
}
