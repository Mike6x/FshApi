using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_emp_dimension_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Titles_TitleId",
                schema: "People",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TitleId",
                schema: "People",
                table: "Employees",
                newName: "DimensionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_TitleId",
                schema: "People",
                table: "Employees",
                newName: "IX_Employees_DimensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Dimensions_DimensionId",
                schema: "People",
                table: "Employees",
                column: "DimensionId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Dimensions_DimensionId",
                schema: "People",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "DimensionId",
                schema: "People",
                table: "Employees",
                newName: "TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DimensionId",
                schema: "People",
                table: "Employees",
                newName: "IX_Employees_TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Titles_TitleId",
                schema: "People",
                table: "Employees",
                column: "TitleId",
                principalSchema: "People",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
