using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    public partial class AddRoleExtendedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleExtendedAttributes",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_RoleExtendedAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleExtendedAttributes_Roles_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleExtendedAttributes_EntityId",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleExtendedAttributes",
                schema: "Identity");
        }
    }
}