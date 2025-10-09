using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesCarts_AspNetUsers_UserId",
                table: "DishesCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_DishesCarts_Dishes_DishId",
                table: "DishesCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_DishesCarts_Orders_OrderId",
                table: "DishesCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishesCarts",
                table: "DishesCarts");

            migrationBuilder.RenameTable(
                name: "DishesCarts",
                newName: "DishesInCarts");

            migrationBuilder.RenameIndex(
                name: "IX_DishesCarts_UserId",
                table: "DishesInCarts",
                newName: "IX_DishesInCarts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DishesCarts_OrderId",
                table: "DishesInCarts",
                newName: "IX_DishesInCarts_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DishesCarts_DishId",
                table: "DishesInCarts",
                newName: "IX_DishesInCarts_DishId");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Ratings",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeleteDate",
                table: "Dishes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishesInCarts",
                table: "DishesInCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesInCarts_AspNetUsers_UserId",
                table: "DishesInCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishesInCarts_Dishes_DishId",
                table: "DishesInCarts",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishesInCarts_Orders_OrderId",
                table: "DishesInCarts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesInCarts_AspNetUsers_UserId",
                table: "DishesInCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_DishesInCarts_Dishes_DishId",
                table: "DishesInCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_DishesInCarts_Orders_OrderId",
                table: "DishesInCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishesInCarts",
                table: "DishesInCarts");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "DishesInCarts",
                newName: "DishesCarts");

            migrationBuilder.RenameIndex(
                name: "IX_DishesInCarts_UserId",
                table: "DishesCarts",
                newName: "IX_DishesCarts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DishesInCarts_OrderId",
                table: "DishesCarts",
                newName: "IX_DishesCarts_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DishesInCarts_DishId",
                table: "DishesCarts",
                newName: "IX_DishesCarts_DishId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeleteDate",
                table: "Dishes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishesCarts",
                table: "DishesCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesCarts_AspNetUsers_UserId",
                table: "DishesCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishesCarts_Dishes_DishId",
                table: "DishesCarts",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishesCarts_Orders_OrderId",
                table: "DishesCarts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
