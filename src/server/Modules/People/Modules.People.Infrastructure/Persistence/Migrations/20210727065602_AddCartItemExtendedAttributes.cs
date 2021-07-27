using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class AddCartItemExtendedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItemExtendedAttributes",
                schema: "People",
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
                    Boolean = table.Column<bool>(type: "boolean", nullable: true),
                    Integer = table.Column<int>(type: "integer", nullable: true),
                    ExternalId = table.Column<string>(type: "text", nullable: true),
                    Group = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemExtendedAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemExtendedAttributes_CartItems_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "People",
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemExtendedAttributes_EntityId",
                schema: "People",
                table: "CartItemExtendedAttributes",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemExtendedAttributes",
                schema: "People");
        }
    }
}
