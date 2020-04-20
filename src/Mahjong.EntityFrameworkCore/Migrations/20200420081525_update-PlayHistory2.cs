using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updatePlayHistory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayHistoryId",
                table: "PlayHistoryDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_PlayHistoryId",
                table: "PlayHistoryDetails",
                column: "PlayHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_PlayHistoreis_PlayHistoryId",
                table: "PlayHistoryDetails",
                column: "PlayHistoryId",
                principalTable: "PlayHistoreis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_PlayHistoreis_PlayHistoryId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_PlayHistoryId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "PlayHistoryId",
                table: "PlayHistoryDetails");
        }
    }
}
