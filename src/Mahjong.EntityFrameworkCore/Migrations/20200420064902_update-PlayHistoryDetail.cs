using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updatePlayHistoryDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_Cards_StaffCardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_StaffCardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "StaffCardId",
                table: "PlayHistoryDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StaffCardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_StaffCardId",
                table: "PlayHistoryDetails",
                column: "StaffCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_Cards_StaffCardId",
                table: "PlayHistoryDetails",
                column: "StaffCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
