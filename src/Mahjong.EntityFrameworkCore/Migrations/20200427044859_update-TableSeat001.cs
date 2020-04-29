using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updateTableSeat001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HelpPlayStaffCardId",
                table: "TableSeats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffPlayerCardId",
                table: "TableSeats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableSeats_HelpPlayStaffCardId",
                table: "TableSeats",
                column: "HelpPlayStaffCardId");

            migrationBuilder.CreateIndex(
                name: "IX_TableSeats_StaffPlayerCardId",
                table: "TableSeats",
                column: "StaffPlayerCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableSeats_Cards_HelpPlayStaffCardId",
                table: "TableSeats",
                column: "HelpPlayStaffCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSeats_Cards_StaffPlayerCardId",
                table: "TableSeats",
                column: "StaffPlayerCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableSeats_Cards_HelpPlayStaffCardId",
                table: "TableSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_TableSeats_Cards_StaffPlayerCardId",
                table: "TableSeats");

            migrationBuilder.DropIndex(
                name: "IX_TableSeats_HelpPlayStaffCardId",
                table: "TableSeats");

            migrationBuilder.DropIndex(
                name: "IX_TableSeats_StaffPlayerCardId",
                table: "TableSeats");

            migrationBuilder.DropColumn(
                name: "HelpPlayStaffCardId",
                table: "TableSeats");

            migrationBuilder.DropColumn(
                name: "StaffPlayerCardId",
                table: "TableSeats");
        }
    }
}
