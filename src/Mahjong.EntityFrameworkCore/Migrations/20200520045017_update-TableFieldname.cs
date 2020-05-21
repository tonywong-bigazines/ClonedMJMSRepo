using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updateTableFieldname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate1",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate2",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate3",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate4",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate5",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate6",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate7",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate8",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayCommissionRate9",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayCommissionRate1",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PayCommissionRate2",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PayCommissionRate3",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PayCommissionRate4",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PayCommissionRate5",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PayCommissionRate6",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PayCommissionRate7",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PayCommissionRate8",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PayCommissionRate9",
                table: "Tables");

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate1",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate2",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate3",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate4",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate5",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate6",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate7",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate8",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate9",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
