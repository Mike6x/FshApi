using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_allocation_tablle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_Dimensions_LeaveTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations");

            migrationBuilder.RenameColumn(
                name: "NumberOfDays",
                schema: "TimeOff",
                table: "LeaveAllocations",
                newName: "NumberOfAnnualDays");

            migrationBuilder.RenameColumn(
                name: "LeaveTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations",
                newName: "LeaveAllocationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_LeaveTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_LeaveAllocationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_Dimensions_LeaveAllocationTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations",
                column: "LeaveAllocationTypeId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_Dimensions_LeaveAllocationTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations");

            migrationBuilder.RenameColumn(
                name: "NumberOfAnnualDays",
                schema: "TimeOff",
                table: "LeaveAllocations",
                newName: "NumberOfDays");

            migrationBuilder.RenameColumn(
                name: "LeaveAllocationTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations",
                newName: "LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_LeaveAllocationTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_Dimensions_LeaveTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
