using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updateTableSeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableSeats_Cards_PlayerCardId",
                table: "TableSeats");

            migrationBuilder.DropIndex(
                name: "IX_TableSeats_PlayerCardId",
                table: "TableSeats");

            migrationBuilder.DropColumn(
                name: "PlayerCardId",
                table: "TableSeats");

            migrationBuilder.DropColumn(
                name: "PlayerType",
                table: "TableSeats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerCardId",
                table: "TableSeats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerType",
                table: "TableSeats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableSeats_PlayerCardId",
                table: "TableSeats",
                column: "PlayerCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableSeats_Cards_PlayerCardId",
                table: "TableSeats",
                column: "PlayerCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
