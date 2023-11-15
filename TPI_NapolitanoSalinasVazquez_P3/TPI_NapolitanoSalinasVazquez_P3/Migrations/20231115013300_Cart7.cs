using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_NapolitanoSalinasVazquez_P3.Migrations
{
    /// <inheritdoc />
    public partial class Cart7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_ProductKeyId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ProductKeyId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "ProductKeyId",
                table: "ShoppingCart",
                newName: "productpId");

            migrationBuilder.AddColumn<int>(
                name: "productID",
                table: "ShoppingCart",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_productID",
                table: "ShoppingCart",
                column: "productID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_productID",
                table: "ShoppingCart",
                column: "productID",
                principalTable: "Product",
                principalColumn: "productID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_productID",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_productID",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "productID",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "productpId",
                table: "ShoppingCart",
                newName: "ProductKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductKeyId",
                table: "ShoppingCart",
                column: "ProductKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_ProductKeyId",
                table: "ShoppingCart",
                column: "ProductKeyId",
                principalTable: "Product",
                principalColumn: "productID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
