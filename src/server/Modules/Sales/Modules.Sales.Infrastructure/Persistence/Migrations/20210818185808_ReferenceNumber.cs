using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Sales.Infrastructure.Persistence.Migrations
{
    public partial class ReferenceNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                schema: "Sales",
                table: "Orders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                schema: "Sales",
                table: "Orders");
        }
    }
}
