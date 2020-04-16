using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahjong.Migrations
{
    public partial class updatetabels0436 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Loser1CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Loser2CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Loser3CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Winner1CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Winner2CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Winner3CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_Loser1CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_Loser2CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_Loser3CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_Winner1CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_Winner2CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlayHistoryDetails_Winner3CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PlayingKey",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Loser1CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Loser1PlayerType",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Loser2CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Loser2PlayerType",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Loser3CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Loser3PlayerType",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Winner1CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Winner1PlayerType",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Winner2CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Winner2PlayerType",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Winner3CardId",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Winner3PlayerType",
                table: "PlayHistoryDetails");

            migrationBuilder.DropColumn(
                name: "PlayingKey",
                table: "PlayHistoreis");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxAmount",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinAmount",
                table: "Tables",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Round",
                table: "Tables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Commission",
                table: "Cards",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PlayHistoryDetailPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayHistoryDetailId = table.Column<int>(nullable: false),
                    PlayerCardId = table.Column<string>(nullable: true),
                    PlayerType = table.Column<string>(nullable: true),
                    StaffCardId = table.Column<string>(nullable: true),
                    WinOrLose = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayHistoryDetailPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetailPlayers_PlayHistoryDetails_PlayHistoryDetailId",
                        column: x => x.PlayHistoryDetailId,
                        principalTable: "PlayHistoryDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetailPlayers_Cards_PlayerCardId",
                        column: x => x.PlayerCardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayHistoryDetailPlayers_Cards_StaffCardId",
                        column: x => x.StaffCardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetailPlayers_PlayHistoryDetailId",
                table: "PlayHistoryDetailPlayers",
                column: "PlayHistoryDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetailPlayers_PlayerCardId",
                table: "PlayHistoryDetailPlayers",
                column: "PlayerCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistoryDetailPlayers_StaffCardId",
                table: "PlayHistoryDetailPlayers",
                column: "StaffCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayHistoryDetailPlayers");

            migrationBuilder.DropColumn(
                name: "MaxAmount",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "MinAmount",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Tables",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayingKey",
                table: "Tables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "PlayHistoryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Loser1CardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Loser1PlayerType",
                table: "PlayHistoryDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Loser2CardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Loser2PlayerType",
                table: "PlayHistoryDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Loser3CardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Loser3PlayerType",
                table: "PlayHistoryDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner1CardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner1PlayerType",
                table: "PlayHistoryDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner2CardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner2PlayerType",
                table: "PlayHistoryDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner3CardId",
                table: "PlayHistoryDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner3PlayerType",
                table: "PlayHistoryDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayingKey",
                table: "PlayHistoreis",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Loser1CardId",
                table: "PlayHistoryDetails",
                column: "Loser1CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Loser2CardId",
                table: "PlayHistoryDetails",
                column: "Loser2CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Loser3CardId",
                table: "PlayHistoryDetails",
                column: "Loser3CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Winner1CardId",
                table: "PlayHistoryDetails",
                column: "Winner1CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Winner2CardId",
                table: "PlayHistoryDetails",
                column: "Winner2CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayHistoryDetails_Cards_Winner3CardId",
                table: "PlayHistoryDetails",
                column: "Winner3CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
