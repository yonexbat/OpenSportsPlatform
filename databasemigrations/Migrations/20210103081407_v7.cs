using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CadenceRpm",
                table: "OSPSample",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HeartRateBpm",
                table: "OSPSample",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "OSPSample",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "OSPSample",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OSPWorkout_UserProfileId",
                table: "OSPWorkout",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_OSPWorkout_OSPUserProfile_UserProfileId",
                table: "OSPWorkout",
                column: "UserProfileId",
                principalTable: "OSPUserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSPWorkout_OSPUserProfile_UserProfileId",
                table: "OSPWorkout");

            migrationBuilder.DropIndex(
                name: "IX_OSPWorkout_UserProfileId",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "CadenceRpm",
                table: "OSPSample");

            migrationBuilder.DropColumn(
                name: "HeartRateBpm",
                table: "OSPSample");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "OSPSample");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "OSPSample");
        }
    }
}
