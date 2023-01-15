using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSPSample_OSPSegment_SegmentId",
                table: "OSPSample");

            migrationBuilder.DropForeignKey(
                name: "FK_OSPSegment_OSPWorkout_WorkoutId",
                table: "OSPSegment");

            migrationBuilder.AddColumn<string>(
                name: "PolarAccessToken",
                table: "OSPUserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PolarAccessTokenValidUntil",
                table: "OSPUserProfile",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OSPSample_OSPSegment_SegmentId",
                table: "OSPSample",
                column: "SegmentId",
                principalTable: "OSPSegment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OSPSegment_OSPWorkout_WorkoutId",
                table: "OSPSegment",
                column: "WorkoutId",
                principalTable: "OSPWorkout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSPSample_OSPSegment_SegmentId",
                table: "OSPSample");

            migrationBuilder.DropForeignKey(
                name: "FK_OSPSegment_OSPWorkout_WorkoutId",
                table: "OSPSegment");

            migrationBuilder.DropColumn(
                name: "PolarAccessToken",
                table: "OSPUserProfile");

            migrationBuilder.DropColumn(
                name: "PolarAccessTokenValidUntil",
                table: "OSPUserProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_OSPSample_OSPSegment_SegmentId",
                table: "OSPSample",
                column: "SegmentId",
                principalTable: "OSPSegment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OSPSegment_OSPWorkout_WorkoutId",
                table: "OSPSegment",
                column: "WorkoutId",
                principalTable: "OSPWorkout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
