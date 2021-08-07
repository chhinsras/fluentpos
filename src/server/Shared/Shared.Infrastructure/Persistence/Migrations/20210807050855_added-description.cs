using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    public partial class addeddescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Application",
                table: "EventLogs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Application",
                table: "EventLogs");
        }
    }
}
