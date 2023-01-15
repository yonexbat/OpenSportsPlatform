using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSportsPlatform.DatabaseMigrations.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PolarUserId",
                table: "OSPUserProfile",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PolarUserId",
                table: "OSPUserProfile");
        }
    }
}
