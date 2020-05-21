using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class addPayCommissionRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayCommissionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    PlayerCardId = table.Column<string>(nullable: true),
                    OperatorCardId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayCommissionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayCommissionRecords_Cards_OperatorCardId",
                        column: x => x.OperatorCardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayCommissionRecords_Cards_PlayerCardId",
                        column: x => x.PlayerCardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayCommissionRecords_OperatorCardId",
                table: "PayCommissionRecords",
                column: "OperatorCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PayCommissionRecords_PlayerCardId",
                table: "PayCommissionRecords",
                column: "PlayerCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayCommissionRecords");
        }
    }
}
