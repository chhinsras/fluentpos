using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    public partial class addedEntityReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.CreateTable(
                name: "EntityReferences",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Entity = table.Column<string>(type: "text", nullable: true),
                    MonthYearString = table.Column<string>(type: "text", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    LastUpdateOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageType = table.Column<string>(type: "text", nullable: true),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EventDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityReferences",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "EventLogs",
                schema: "Application");
        }
    }
}
