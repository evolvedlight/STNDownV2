using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace STNDownV2.Data.Migrations
{
    public partial class AddEvents_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerCheckerEvent_CheckedServer_ServerID",
                table: "ServerCheckerEvent");

            migrationBuilder.DropIndex(
                name: "IX_ServerCheckerEvent_ServerID",
                table: "ServerCheckerEvent");

            migrationBuilder.DropColumn(
                name: "ServerID",
                table: "ServerCheckerEvent");

            migrationBuilder.AddColumn<int>(
                name: "CheckedServerId",
                table: "ServerCheckerEvent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServerCheckerEvent_CheckedServerId",
                table: "ServerCheckerEvent",
                column: "CheckedServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerCheckerEvent_CheckedServer_CheckedServerId",
                table: "ServerCheckerEvent",
                column: "CheckedServerId",
                principalTable: "CheckedServer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerCheckerEvent_CheckedServer_CheckedServerId",
                table: "ServerCheckerEvent");

            migrationBuilder.DropIndex(
                name: "IX_ServerCheckerEvent_CheckedServerId",
                table: "ServerCheckerEvent");

            migrationBuilder.DropColumn(
                name: "CheckedServerId",
                table: "ServerCheckerEvent");

            migrationBuilder.AddColumn<int>(
                name: "ServerID",
                table: "ServerCheckerEvent",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerCheckerEvent_ServerID",
                table: "ServerCheckerEvent",
                column: "ServerID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerCheckerEvent_CheckedServer_ServerID",
                table: "ServerCheckerEvent",
                column: "ServerID",
                principalTable: "CheckedServer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
