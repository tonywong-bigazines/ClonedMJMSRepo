using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updatetabels2004160445 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableSeats_Cards_TableCardId",
                table: "TableSeats");

            migrationBuilder.DropIndex(
                name: "IX_TableSeats_TableCardId",
                table: "TableSeats");

            migrationBuilder.DropColumn(
                name: "TableCardId",
                table: "TableSeats");

            migrationBuilder.AddColumn<string>(
                name: "PlayerCardId",
                table: "TableSeats",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "TableCardId",
                table: "TableSeats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableSeats_TableCardId",
                table: "TableSeats",
                column: "TableCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableSeats_Cards_TableCardId",
                table: "TableSeats",
                column: "TableCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
