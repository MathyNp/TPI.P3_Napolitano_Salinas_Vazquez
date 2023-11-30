﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_NapolitanoSalinasVazquez_P3.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductNames",
                table: "Histories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNames",
                table: "Histories");
        }
    }
}
