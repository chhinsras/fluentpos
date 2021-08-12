using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Sales.Infrastructure.Persistence.Migrations
{
    public partial class ProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "Sales",
                table: "Products");

            migrationBuilder.AlterColumn<byte>(
                name: "PaymentType",
                schema: "Sales",
                table: "Transactions",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Sales",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "Sales",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                schema: "Sales",
                table: "Products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                schema: "Sales",
                table: "Products",
                column: "OrderId",
                principalSchema: "Sales",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                schema: "Sales",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "Sales",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                schema: "Sales",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Sales",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                schema: "Sales",
                table: "Transactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "Sales",
                table: "Products",
                column: "ProductId");
        }
    }
}
