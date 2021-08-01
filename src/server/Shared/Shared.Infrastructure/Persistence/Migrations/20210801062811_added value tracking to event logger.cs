using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    public partial class addedvaluetrackingtoeventlogger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreviousData",
                schema: "Application",
                table: "EventLogs",
                newName: "OldValues");

            migrationBuilder.AddColumn<string>(
                name: "NewValues",
                schema: "Application",
                table: "EventLogs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValues",
                schema: "Application",
                table: "EventLogs");

            migrationBuilder.RenameColumn(
                name: "OldValues",
                schema: "Application",
                table: "EventLogs",
                newName: "PreviousData");
        }
    }
}
