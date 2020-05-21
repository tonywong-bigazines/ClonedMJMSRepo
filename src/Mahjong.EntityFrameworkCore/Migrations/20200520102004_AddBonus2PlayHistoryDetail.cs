using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class AddBonus2PlayHistoryDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bonus",
                table: "PlayHistoryDetailPlayers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "PlayHistoryDetailPlayers");
        }
    }
}
