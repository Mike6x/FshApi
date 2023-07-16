using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class add_timeoff_related_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TimeOff");

            migrationBuilder.CreateTable(
                name: "LeaveAllocations",
                schema: "TimeOff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberOfDays = table.Column<int>(type: "integer", nullable: false),
                    NumberOfExtraDays = table.Column<int>(type: "integer", nullable: false),
                    NumberOfCarryOverDays = table.Column<double>(type: "double precision", nullable: false),
                    NumberOfCompensationDays = table.Column<double>(type: "double precision", nullable: false),
                    NumberOfValidDays = table.Column<double>(type: "double precision", nullable: false),
                    NumberOfOnHandDays = table.Column<double>(type: "double precision", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_Dimensions_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "Settings",
                        principalTable: "Dimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "People",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveApplications",
                schema: "TimeOff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FirstLeaveDay = table.Column<int>(type: "integer", nullable: false),
                    LastLeaveDay = table.Column<int>(type: "integer", nullable: false),
                    RequestOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestRemarks = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ApproverId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApproverRemarks = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LeaveAllocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberOfValidDays = table.Column<double>(type: "double precision", nullable: true),
                    NumberOfOnHandDays = table.Column<double>(type: "double precision", nullable: true),
                    TenantId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Dimensions_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "Settings",
                        principalTable: "Dimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Employees_ApproverId",
                        column: x => x.ApproverId,
                        principalSchema: "People",
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "People",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_LeaveAllocations_LeaveAllocationId",
                        column: x => x.LeaveAllocationId,
                        principalSchema: "TimeOff",
                        principalTable: "LeaveAllocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                schema: "TimeOff",
                table: "LeaveAllocations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_LeaveTypeId",
                schema: "TimeOff",
                table: "LeaveAllocations",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_ApproverId",
                schema: "TimeOff",
                table: "LeaveApplications",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_EmployeeId",
                schema: "TimeOff",
                table: "LeaveApplications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_LeaveAllocationId",
                schema: "TimeOff",
                table: "LeaveApplications",
                column: "LeaveAllocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_LeaveTypeId",
                schema: "TimeOff",
                table: "LeaveApplications",
                column: "LeaveTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveApplications",
                schema: "TimeOff");

            migrationBuilder.DropTable(
                name: "LeaveAllocations",
                schema: "TimeOff");
        }
    }
}
