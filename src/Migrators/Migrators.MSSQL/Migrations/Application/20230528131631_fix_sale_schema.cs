using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_sale_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "Catalog",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Staff_StaffId",
                schema: "Catalog",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Stores_StoreId",
                schema: "Catalog",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Staff_ManagerId",
                schema: "Catalog",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Stores_StoreId",
                schema: "Catalog",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Products_ProductId",
                schema: "Catalog",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Stores_StoreId",
                schema: "Catalog",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                schema: "Catalog",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                schema: "Catalog",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "Catalog",
                table: "Order");

            migrationBuilder.EnsureSchema(
                name: "Sale");

            migrationBuilder.RenameTable(
                name: "Stock",
                schema: "Catalog",
                newName: "Stocks",
                newSchema: "Sale");

            migrationBuilder.RenameTable(
                name: "Staff",
                schema: "Catalog",
                newName: "Staffs",
                newSchema: "Sale");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "Catalog",
                newName: "OrderItems",
                newSchema: "Sale");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "Catalog",
                newName: "Orders",
                newSchema: "Sale");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_StoreId",
                schema: "Sale",
                table: "Stocks",
                newName: "IX_Stocks_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_ProductId",
                schema: "Sale",
                table: "Stocks",
                newName: "IX_Stocks_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_StoreId",
                schema: "Sale",
                table: "Staffs",
                newName: "IX_Staffs_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_ManagerId",
                schema: "Sale",
                table: "Staffs",
                newName: "IX_Staffs_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ProductId",
                schema: "Sale",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Sale",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StoreId",
                schema: "Sale",
                table: "Orders",
                newName: "IX_Orders_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StaffId",
                schema: "Sale",
                table: "Orders",
                newName: "IX_Orders_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                schema: "Sale",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Sale",
                table: "Stocks",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Sale",
                table: "Staffs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Sale",
                table: "OrderItems",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Sale",
                table: "Orders",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Sale",
                table: "Orders",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                schema: "Sale",
                table: "Stocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                schema: "Sale",
                table: "Staffs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                schema: "Sale",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                schema: "Sale",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                schema: "Sale",
                table: "OrderItems",
                column: "OrderId",
                principalSchema: "Sale",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                schema: "Sale",
                table: "OrderItems",
                column: "ProductId",
                principalSchema: "Production",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                schema: "Sale",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "Catalog",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staffs_StaffId",
                schema: "Sale",
                table: "Orders",
                column: "StaffId",
                principalSchema: "Sale",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stores_StoreId",
                schema: "Sale",
                table: "Orders",
                column: "StoreId",
                principalSchema: "Place",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Staffs_ManagerId",
                schema: "Sale",
                table: "Staffs",
                column: "ManagerId",
                principalSchema: "Sale",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Stores_StoreId",
                schema: "Sale",
                table: "Staffs",
                column: "StoreId",
                principalSchema: "Place",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_ProductId",
                schema: "Sale",
                table: "Stocks",
                column: "ProductId",
                principalSchema: "Production",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Stores_StoreId",
                schema: "Sale",
                table: "Stocks",
                column: "StoreId",
                principalSchema: "Place",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                schema: "Sale",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                schema: "Sale",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                schema: "Sale",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staffs_StaffId",
                schema: "Sale",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stores_StoreId",
                schema: "Sale",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Staffs_ManagerId",
                schema: "Sale",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Stores_StoreId",
                schema: "Sale",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_ProductId",
                schema: "Sale",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Stores_StoreId",
                schema: "Sale",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                schema: "Sale",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                schema: "Sale",
                table: "Staffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "Sale",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                schema: "Sale",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "Sale",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "Sale",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "Sale",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "Sale",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Stocks",
                schema: "Sale",
                newName: "Stock",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Staffs",
                schema: "Sale",
                newName: "Staff",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "Sale",
                newName: "Order",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                schema: "Sale",
                newName: "OrderItem",
                newSchema: "Catalog");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_StoreId",
                schema: "Catalog",
                table: "Stock",
                newName: "IX_Stock_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_ProductId",
                schema: "Catalog",
                table: "Stock",
                newName: "IX_Stock_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_StoreId",
                schema: "Catalog",
                table: "Staff",
                newName: "IX_Staff_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_ManagerId",
                schema: "Catalog",
                table: "Staff",
                newName: "IX_Staff_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StoreId",
                schema: "Catalog",
                table: "Order",
                newName: "IX_Order_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StaffId",
                schema: "Catalog",
                table: "Order",
                newName: "IX_Order_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                schema: "Catalog",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductId",
                schema: "Catalog",
                table: "OrderItem",
                newName: "IX_OrderItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                schema: "Catalog",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Catalog",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                schema: "Catalog",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                schema: "Catalog",
                table: "Staff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "Catalog",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "Catalog",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "Catalog",
                table: "Order",
                column: "CustomerId",
                principalSchema: "Catalog",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Staff_StaffId",
                schema: "Catalog",
                table: "Order",
                column: "StaffId",
                principalSchema: "Catalog",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Stores_StoreId",
                schema: "Catalog",
                table: "Order",
                column: "StoreId",
                principalSchema: "Place",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Catalog",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "Catalog",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                schema: "Catalog",
                table: "OrderItem",
                column: "ProductId",
                principalSchema: "Production",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Staff_ManagerId",
                schema: "Catalog",
                table: "Staff",
                column: "ManagerId",
                principalSchema: "Catalog",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Stores_StoreId",
                schema: "Catalog",
                table: "Staff",
                column: "StoreId",
                principalSchema: "Place",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Products_ProductId",
                schema: "Catalog",
                table: "Stock",
                column: "ProductId",
                principalSchema: "Production",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Stores_StoreId",
                schema: "Catalog",
                table: "Stock",
                column: "StoreId",
                principalSchema: "Place",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
