using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_customer_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                schema: "Sale",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                schema: "Catalog",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "Catalog",
                newName: "Customers",
                newSchema: "Sale");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Sale",
                table: "Customers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Sale",
                table: "Customers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                schema: "Sale",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "Sale",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "Sale",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "Sale",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                schema: "Sale",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "Sale",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Sale",
                newName: "Customer",
                newSchema: "Catalog");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Catalog",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                schema: "Catalog",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                schema: "Sale",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "Catalog",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}
