using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    public partial class AddBrandExtendedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandExtendedAttributes",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Decimal = table.Column<decimal>(type: "numeric", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Json = table.Column<string>(type: "text", nullable: true),
                    ExternalId = table.Column<string>(type: "text", nullable: true),
                    Group = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandExtendedAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandExtendedAttributes_Brands_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "Catalog",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandExtendedAttributes_EntityId",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandExtendedAttributes",
                schema: "Catalog");
        }
    }
}
