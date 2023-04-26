using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_Product_table_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategorieId",
                schema: "Production",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategorieId",
                schema: "Production",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategorieId",
                schema: "Production",
                table: "Products",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategorieId",
                schema: "Production",
                table: "Products",
                column: "SubCategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategorieId",
                schema: "Production",
                table: "Products",
                column: "CategorieId",
                principalSchema: "Catalog",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategorieId",
                schema: "Production",
                table: "Products",
                column: "SubCategorieId",
                principalSchema: "Catalog",
                principalTable: "SubCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategorieId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategorieId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategorieId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategorieId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubCategorieId",
                schema: "Production",
                table: "Products");
        }
    }
}
