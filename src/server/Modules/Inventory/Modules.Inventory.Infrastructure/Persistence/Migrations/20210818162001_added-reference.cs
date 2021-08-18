using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    public partial class addedreference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                schema: "Inventory",
                table: "StockTransactions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                schema: "Inventory",
                table: "StockTransactions");
        }
    }
}
