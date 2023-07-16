using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class add_cronjob_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfMonth",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                schema: "Settings",
                table: "BackgroundJobs",
                newName: "ToDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RunTime",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDate",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropColumn(
                name: "RunTime",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.RenameColumn(
                name: "ToDate",
                schema: "Settings",
                table: "BackgroundJobs",
                newName: "StartTime");

            migrationBuilder.AddColumn<int>(
                name: "DayOfMonth",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
