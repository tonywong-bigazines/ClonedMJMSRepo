using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updatePlayHsitoryDetail3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OperatorCardId",
                table: "PlayHistoryDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_OperatorCardId",
                table: "PlayHistoryDetails",
                column: "OperatorCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_Cards_OperatorCardId",
                table: "PlayHistoryDetails",
                column: "OperatorCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_Cards_OperatorCardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_OperatorCardId",
                table: "PlayHistoryDetails");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorCardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
