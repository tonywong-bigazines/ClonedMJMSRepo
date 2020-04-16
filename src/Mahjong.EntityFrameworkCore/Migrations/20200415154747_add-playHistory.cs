using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class addplayHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlayingKey",
                table: "Tables",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PlayHistoreis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(nullable: false),
                    Round = table.Column<int>(nullable: false),
                    PlayingKey = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayHistoreis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayHistoreis_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayHistoryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Winner1CardId = table.Column<string>(nullable: true),
                    Winner1PlayerType = table.Column<string>(nullable: true),
                    Winner2CardId = table.Column<string>(nullable: true),
                    Winner2PlayerType = table.Column<string>(nullable: true),
                    Winner3CardId = table.Column<string>(nullable: true),
                    Winner3PlayerType = table.Column<string>(nullable: true),
                    Loser1CardId = table.Column<string>(nullable: true),
                    Loser1PlayerType = table.Column<string>(nullable: true),
                    Loser2CardId = table.Column<string>(nullable: true),
                    Loser2PlayerType = table.Column<string>(nullable: true),
                    Loser3CardId = table.Column<string>(nullable: true),
                    Loser3PlayerType = table.Column<string>(nullable: true),
                    MohjongActionName = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    StaffCardId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayHistoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetails_Cards_Loser1CardId",
                        column: x => x.Loser1CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetails_Cards_Loser2CardId",
                        column: x => x.Loser2CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetails_Cards_Loser3CardId",
                        column: x => x.Loser3CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetails_Cards_StaffCardId",
                        column: x => x.StaffCardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetails_Cards_Winner1CardId",
                        column: x => x.Winner1CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetails_Cards_Winner2CardId",
                        column: x => x.Winner2CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetails_Cards_Winner3CardId",
                        column: x => x.Winner3CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoreis_TableId",
                table: "PlayHistoreis",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_Loser1CardId",
                table: "PlayHistoryDetails",
                column: "Loser1CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_Loser2CardId",
                table: "PlayHistoryDetails",
                column: "Loser2CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_Loser3CardId",
                table: "PlayHistoryDetails",
                column: "Loser3CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_StaffCardId",
                table: "PlayHistoryDetails",
                column: "StaffCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_Winner1CardId",
                table: "PlayHistoryDetails",
                column: "Winner1CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_Winner2CardId",
                table: "PlayHistoryDetails",
                column: "Winner2CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetails_Winner3CardId",
                table: "PlayHistoryDetails",
                column: "Winner3CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayHistoreis");

            migrationBuilder.DropTable(
                name: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "PlayingKey",
                table: "Tables");
        }
    }
}
