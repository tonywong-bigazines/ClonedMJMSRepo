using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class AddHelpPlayInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HelpPlayAmount",
                table: "TableSeats",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "HelpPlaying",
                table: "TableSeats",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Round",
                table: "TableSeats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LoserAmountFormula",
                table: "MahJongActions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelpPlayAmount",
                table: "TableSeats");

            migrationBuilder.DropColumn(
                name: "HelpPlaying",
                table: "TableSeats");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "TableSeats");

            migrationBuilder.DropColumn(
                name: "LoserAmountFormula",
                table: "MahJongActions");
        }
    }
}
