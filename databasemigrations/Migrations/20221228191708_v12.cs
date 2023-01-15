using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSPWorkout_OSPSportcCategory_SportsCategoryId",
                table: "OSPWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_OSPWorkout_OSPUserProfile_UserProfileId",
                table: "OSPWorkout");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                table: "OSPWorkout",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPWorkout",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "SportsCategoryId",
                table: "OSPWorkout",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPWorkout",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPUserProfile",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "OSPUserProfile",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPUserProfile",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPSportcCategory",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPSportcCategory",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPSegment",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPSegment",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPSample",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPSample",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.CreateTable(
                name: "OSPTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSPTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OSPTagWorkout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSPTagWorkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OSPTagWorkout_OSPTag_TagId",
                        column: x => x.TagId,
                        principalTable: "OSPTag",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OSPTagWorkout_OSPWorkout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "OSPWorkout",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OSPTagWorkout_TagId",
                table: "OSPTagWorkout",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_OSPTagWorkout_WorkoutId",
                table: "OSPTagWorkout",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_OSPWorkout_OSPSportcCategory_SportsCategoryId",
                table: "OSPWorkout",
                column: "SportsCategoryId",
                principalTable: "OSPSportcCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OSPWorkout_OSPUserProfile_UserProfileId",
                table: "OSPWorkout",
                column: "UserProfileId",
                principalTable: "OSPUserProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSPWorkout_OSPSportcCategory_SportsCategoryId",
                table: "OSPWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_OSPWorkout_OSPUserProfile_UserProfileId",
                table: "OSPWorkout");

            migrationBuilder.DropTable(
                name: "OSPTagWorkout");

            migrationBuilder.DropTable(
                name: "OSPTag");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                table: "OSPWorkout",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPWorkout",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SportsCategoryId",
                table: "OSPWorkout",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPWorkout",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPUserProfile",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "OSPUserProfile",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPUserProfile",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPSportcCategory",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPSportcCategory",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPSegment",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPSegment",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "OSPSample",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "InsertDate",
                table: "OSPSample",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OSPWorkout_OSPSportcCategory_SportsCategoryId",
                table: "OSPWorkout",
                column: "SportsCategoryId",
                principalTable: "OSPSportcCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OSPWorkout_OSPUserProfile_UserProfileId",
                table: "OSPWorkout",
                column: "UserProfileId",
                principalTable: "OSPUserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
