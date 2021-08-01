using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    public partial class addedprviousdatatoeventlogger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousData",
                schema: "Application",
                table: "EventLogs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousData",
                schema: "Application",
                table: "EventLogs");
        }
    }
}
