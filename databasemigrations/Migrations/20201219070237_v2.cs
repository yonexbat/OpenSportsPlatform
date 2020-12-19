using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AltitudeMaxInMeters",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AltitudeMinInMeters",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AscendInMeters",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CadenceAvgRpm",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CadenceMaxRpm",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CaloriesInKCal",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DescendInMeters",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DistanceInKm",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DurationInSec",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HeartRateAvgBpm",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HeartRateMaxBpm",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SpeedAvgKmh",
                table: "OSPWorkout",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SpeedMaxKmh",
                table: "OSPWorkout",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltitudeMaxInMeters",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "AltitudeMinInMeters",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "AscendInMeters",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "CadenceAvgRpm",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "CadenceMaxRpm",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "CaloriesInKCal",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "DescendInMeters",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "DistanceInKm",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "DurationInSec",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "HeartRateAvgBpm",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "HeartRateMaxBpm",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "SpeedAvgKmh",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "SpeedMaxKmh",
                table: "OSPWorkout");
        }
    }
}
