using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class add_starttime_to_BJob_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeSpan",
                schema: "Settings",
                table: "BackgroundJobs",
                newName: "RepeatTimes");

            migrationBuilder.AddColumn<int>(
                name: "Command",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfMonth",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "FatherId",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                schema: "Settings",
                table: "BackgroundJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundJobs_FatherId",
                schema: "Settings",
                table: "BackgroundJobs",
                column: "FatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackgroundJobs_BackgroundJobs_FatherId",
                schema: "Settings",
                table: "BackgroundJobs",
                column: "FatherId",
                principalSchema: "Settings",
                principalTable: "BackgroundJobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackgroundJobs_BackgroundJobs_FatherId",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropIndex(
                name: "IX_BackgroundJobs_FatherId",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropColumn(
                name: "Command",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropColumn(
                name: "DayOfMonth",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropColumn(
                name: "FatherId",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.DropColumn(
                name: "StartTime",
                schema: "Settings",
                table: "BackgroundJobs");

            migrationBuilder.RenameColumn(
                name: "RepeatTimes",
                schema: "Settings",
                table: "BackgroundJobs",
                newName: "TimeSpan");
        }
    }
}
