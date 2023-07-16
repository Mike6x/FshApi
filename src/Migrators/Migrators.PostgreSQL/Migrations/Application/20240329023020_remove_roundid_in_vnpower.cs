using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class remove_roundid_in_vnpower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundId",
                schema: "Game",
                table: "VnPowers");

            migrationBuilder.DropColumn(
                name: "SubRoundId",
                schema: "Game",
                table: "VnPowers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoundId",
                schema: "Game",
                table: "VnPowers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubRoundId",
                schema: "Game",
                table: "VnPowers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
