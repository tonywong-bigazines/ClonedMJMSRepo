using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class UpdateMajongAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoserAmountFormula",
                table: "MahJongActions");

            migrationBuilder.AddColumn<string>(
                name: "LoseAmountFormula",
                table: "MahJongActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WinAmountFormula",
                table: "MahJongActions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoseAmountFormula",
                table: "MahJongActions");

            migrationBuilder.DropColumn(
                name: "WinAmountFormula",
                table: "MahJongActions");

            migrationBuilder.AddColumn<string>(
                name: "LoserAmountFormula",
                table: "MahJongActions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
