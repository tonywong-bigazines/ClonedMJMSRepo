using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updatePlayHsitoryDetail2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaffCardId",
                table: "PlayHistoryDetails");

            migrationBuilder.AddColumn<string>(
                name: "OperatorCardId",
                table: "PlayHistoryDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperatorCardId",
                table: "PlayHistoryDetails");

            migrationBuilder.AddColumn<string>(
                name: "StaffCardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
