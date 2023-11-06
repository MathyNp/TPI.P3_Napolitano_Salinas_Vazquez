using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_NapolitanoSalinasVazquez_P3.Migrations
{
    /// <inheritdoc />
    public partial class UserShoppingCartv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCartId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserCartId",
                table: "Users",
                column: "UserCartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ShoppingCart_UserCartId",
                table: "Users",
                column: "UserCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ShoppingCart_UserCartId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserCartId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserCartId",
                table: "Users");
        }
    }
}
