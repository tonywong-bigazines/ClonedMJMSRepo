using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updateTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate1",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate2",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate3",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate4",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate5",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate6",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate7",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate8",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate9",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionRate1",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CommissionRate2",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CommissionRate3",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CommissionRate4",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CommissionRate5",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CommissionRate6",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CommissionRate7",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CommissionRate8",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CommissionRate9",
                table: "Tables");
        }
    }
}
