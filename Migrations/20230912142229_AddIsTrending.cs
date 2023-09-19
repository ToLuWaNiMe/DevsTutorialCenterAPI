using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevsTutorialCenterAPI.Migrations
{
    public partial class AddIsTrending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTrending",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrending",
                table: "Articles");
        }
    }
}
