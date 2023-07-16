using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class add_apiserial_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Integrations");

            migrationBuilder.CreateTable(
                name: "CronJobs",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RunTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalRecord = table.Column<int>(type: "integer", nullable: false),
                    NumberOfSuccessed = table.Column<int>(type: "integer", nullable: false),
                    NumberOfFailed = table.Column<int>(type: "integer", nullable: false),
                    NumberOfDuplicated = table.Column<int>(type: "integer", nullable: false),
                    NumberOfExisted = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CronJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiSerials",
                schema: "Integrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemSerial = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ItemCode = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ItemName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ItemClass = table.Column<string>(type: "text", nullable: false),
                    ItemBrand = table.Column<string>(type: "text", nullable: false),
                    PoNumber = table.Column<string>(type: "text", nullable: false),
                    PoStatus = table.Column<string>(type: "text", nullable: false),
                    PoCreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PoModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PoProcessStatus = table.Column<string>(type: "text", nullable: false),
                    CustomStatusSys = table.Column<string>(type: "text", nullable: false),
                    CustomStatusIbsm = table.Column<string>(type: "text", nullable: false),
                    ImportStatus = table.Column<string>(type: "text", nullable: true),
                    CronJobId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_ApiSerials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiSerials_CronJobs_CronJobId",
                        column: x => x.CronJobId,
                        principalSchema: "Settings",
                        principalTable: "CronJobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiSerials_CronJobId",
                schema: "Integrations",
                table: "ApiSerials",
                column: "CronJobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiSerials",
                schema: "Integrations");

            migrationBuilder.DropTable(
                name: "CronJobs",
                schema: "Settings");
        }
    }
}
