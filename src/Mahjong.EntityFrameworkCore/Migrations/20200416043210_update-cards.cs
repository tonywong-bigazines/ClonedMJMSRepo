using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updatecards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Tables_TableId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_TableId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Cards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_TableId",
                table: "Cards",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Tables_TableId",
                table: "Cards",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
