using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class AddBooleanAndIntegerToExtendedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Boolean",
                schema: "People",
                table: "CustomerExtendedAttributes",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Integer",
                schema: "People",
                table: "CustomerExtendedAttributes",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boolean",
                schema: "People",
                table: "CustomerExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Integer",
                schema: "People",
                table: "CustomerExtendedAttributes");
        }
    }
}
