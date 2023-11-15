using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_NapolitanoSalinasVazquez_P3.Migrations
{
    /// <inheritdoc />
    public partial class Cart8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_productID",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "productpId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "productID",
                table: "ShoppingCart",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_productID",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_productId",
                table: "ShoppingCart",
                column: "productId",
                principalTable: "Product",
                principalColumn: "productID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_productId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ShoppingCart",
                newName: "productID");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_productId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_productID");

            migrationBuilder.AddColumn<int>(
                name: "productpId",
                table: "ShoppingCart",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_productID",
                table: "ShoppingCart",
                column: "productID",
                principalTable: "Product",
                principalColumn: "productID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
