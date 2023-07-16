using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_application_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_LeaveAllocations_LeaveAllocationId",
                schema: "TimeOff",
                table: "LeaveApplications");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeaveAllocationId",
                schema: "TimeOff",
                table: "LeaveApplications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_LeaveAllocations_LeaveAllocationId",
                schema: "TimeOff",
                table: "LeaveApplications",
                column: "LeaveAllocationId",
                principalSchema: "TimeOff",
                principalTable: "LeaveAllocations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_LeaveAllocations_LeaveAllocationId",
                schema: "TimeOff",
                table: "LeaveApplications");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeaveAllocationId",
                schema: "TimeOff",
                table: "LeaveApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_LeaveAllocations_LeaveAllocationId",
                schema: "TimeOff",
                table: "LeaveApplications",
                column: "LeaveAllocationId",
                principalSchema: "TimeOff",
                principalTable: "LeaveAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
