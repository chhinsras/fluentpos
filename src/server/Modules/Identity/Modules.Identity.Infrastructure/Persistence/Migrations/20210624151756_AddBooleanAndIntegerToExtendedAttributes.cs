using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    public partial class AddBooleanAndIntegerToExtendedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Boolean",
                schema: "Identity",
                table: "UserExtendedAttributes",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Integer",
                schema: "Identity",
                table: "UserExtendedAttributes",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boolean",
                schema: "Identity",
                table: "UserExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Integer",
                schema: "Identity",
                table: "UserExtendedAttributes");
        }
    }
}
