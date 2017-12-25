using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace STNDownV2.Data.Migrations
{
    public partial class AddEvents_ExtendLatencyField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Latency",
                table: "ServerCheckerEvent",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Latency",
                table: "ServerCheckerEvent",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
