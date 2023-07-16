using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class add_jactpot_value_vnpower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JackpotAmount",
                schema: "Game",
                table: "VnPowers",
                newName: "Jackpot2Value");

            migrationBuilder.AddColumn<decimal>(
                name: "Jackpot1Value",
                schema: "Game",
                table: "VnPowers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jackpot1Value",
                schema: "Game",
                table: "VnPowers");

            migrationBuilder.RenameColumn(
                name: "Jackpot2Value",
                schema: "Game",
                table: "VnPowers",
                newName: "JackpotAmount");
        }
    }
}
