using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_vnpowerresult_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoundId",
                schema: "Game",
                table: "VnPowerResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubRoundId",
                schema: "Game",
                table: "VnPowerResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundId",
                schema: "Game",
                table: "VnPowerResults");

            migrationBuilder.DropColumn(
                name: "SubRoundId",
                schema: "Game",
                table: "VnPowerResults");
        }
    }
}
