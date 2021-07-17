using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    public partial class AddBooleanAndIntegerToExtendedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Boolean",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Integer",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Boolean",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Integer",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Boolean",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Integer",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boolean",
                schema: "Catalog",
                table: "ProductExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Integer",
                schema: "Catalog",
                table: "ProductExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Boolean",
                schema: "Catalog",
                table: "CategoryExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Integer",
                schema: "Catalog",
                table: "CategoryExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Boolean",
                schema: "Catalog",
                table: "BrandExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Integer",
                schema: "Catalog",
                table: "BrandExtendedAttributes");
        }
    }
}