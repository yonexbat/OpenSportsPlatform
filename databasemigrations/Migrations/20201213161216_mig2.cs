using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "OSPWorkout",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "OSPWorkout",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "OSPWorkout",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SportsCategoryId",
                table: "OSPWorkout",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "OSPWorkout",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "OSPWorkout",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "OSPWorkout",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OSPSportcCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSPSportcCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OSPWorkout_SportsCategoryId",
                table: "OSPWorkout",
                column: "SportsCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OSPWorkout_OSPSportcCategory_SportsCategoryId",
                table: "OSPWorkout",
                column: "SportsCategoryId",
                principalTable: "OSPSportcCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSPWorkout_OSPSportcCategory_SportsCategoryId",
                table: "OSPWorkout");

            migrationBuilder.DropTable(
                name: "OSPSportcCategory");

            migrationBuilder.DropIndex(
                name: "IX_OSPWorkout_SportsCategoryId",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "SportsCategoryId",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "OSPWorkout");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "OSPWorkout");
        }
    }
}
