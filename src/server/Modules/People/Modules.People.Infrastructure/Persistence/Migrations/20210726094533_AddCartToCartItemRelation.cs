using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class AddCartToCartItemRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                schema: "People",
                table: "CartItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                schema: "People",
                table: "CartItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                schema: "People",
                table: "CartItems",
                column: "CartId",
                principalSchema: "People",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                schema: "People",
                table: "CartItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                schema: "People",
                table: "CartItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                schema: "People",
                table: "CartItems",
                column: "CartId",
                principalSchema: "People",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
