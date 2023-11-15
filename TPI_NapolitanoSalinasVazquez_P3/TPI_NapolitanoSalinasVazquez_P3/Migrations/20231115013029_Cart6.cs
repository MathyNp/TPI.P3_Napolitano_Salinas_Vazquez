using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_NapolitanoSalinasVazquez_P3.Migrations
{
    /// <inheritdoc />
    public partial class Cart6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_ProductForeignKeyId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "ProductForeignKeyId",
                table: "ShoppingCart",
                newName: "ProductKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_ProductForeignKeyId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_ProductKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_ProductKeyId",
                table: "ShoppingCart",
                column: "ProductKeyId",
                principalTable: "Product",
                principalColumn: "productID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_ProductKeyId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "ProductKeyId",
                table: "ShoppingCart",
                newName: "ProductForeignKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_ProductKeyId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_ProductForeignKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_ProductForeignKeyId",
                table: "ShoppingCart",
                column: "ProductForeignKeyId",
                principalTable: "Product",
                principalColumn: "productID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
