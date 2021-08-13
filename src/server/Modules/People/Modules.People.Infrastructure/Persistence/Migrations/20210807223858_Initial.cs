using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "People");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "People",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerExtendedAttributes",
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
                    table.PrimaryKey("PK_CustomerExtendedAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerExtendedAttributes_Customers_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "People",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartExtendedAttributes",
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
                    table.PrimaryKey("PK_CartExtendedAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartExtendedAttributes_Carts_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "People",
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CartId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "People",
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CartExtendedAttributes_EntityId",
                schema: "People",
                table: "CartExtendedAttributes",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemExtendedAttributes_EntityId",
                schema: "People",
                table: "CartItemExtendedAttributes",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                schema: "People",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                schema: "People",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerExtendedAttributes_EntityId",
                schema: "People",
                table: "CustomerExtendedAttributes",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartExtendedAttributes",
                schema: "People");

            migrationBuilder.DropTable(
                name: "CartItemExtendedAttributes",
                schema: "People");

            migrationBuilder.DropTable(
                name: "CustomerExtendedAttributes",
                schema: "People");

            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "People");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "People");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "People");
        }
    }
}
