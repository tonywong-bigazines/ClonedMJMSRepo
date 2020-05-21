using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class AddCommissionRate2Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Tables");
        }
    }
}
