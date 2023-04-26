using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
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
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Sale",
                table: "Customers",
                type: "character varying(64)",
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
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
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
