using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    public partial class addeduseridtoeventlogger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                schema: "Application",
                table: "EventLogs",
                newName: "Email");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Application",
                table: "EventLogs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Application",
                table: "EventLogs");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "Application",
                table: "EventLogs",
                newName: "User");
        }
    }
}
