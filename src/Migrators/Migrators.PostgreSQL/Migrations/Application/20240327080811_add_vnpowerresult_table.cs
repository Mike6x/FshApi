using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class add_vnpowerresult_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VnPowerForcasts",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number1 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number2 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number3 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number4 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number5 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number6 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number7 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number8 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number9 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number10 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number11 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number12 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number13 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number14 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number15 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number16 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number17 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number18 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number19 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number20 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number21 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number22 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number23 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number24 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number25 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number26 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number27 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number28 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number29 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number30 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number31 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number32 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number33 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number34 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number35 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number36 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number37 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number38 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number39 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number40 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number41 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number42 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number43 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number44 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number45 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number46 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number47 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number48 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number49 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number50 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number51 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number52 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number53 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number54 = table.Column<decimal>(type: "numeric", nullable: false),
                    Number55 = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VnPowerForcasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VnPowerForcasts_VnPowers_Id",
                        column: x => x.Id,
                        principalSchema: "Game",
                        principalTable: "VnPowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VnPowerResults",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number1 = table.Column<int>(type: "integer", nullable: false),
                    Number2 = table.Column<int>(type: "integer", nullable: false),
                    Number3 = table.Column<int>(type: "integer", nullable: false),
                    Number4 = table.Column<int>(type: "integer", nullable: false),
                    Number5 = table.Column<int>(type: "integer", nullable: false),
                    Number6 = table.Column<int>(type: "integer", nullable: false),
                    Number7 = table.Column<int>(type: "integer", nullable: false),
                    Number8 = table.Column<int>(type: "integer", nullable: false),
                    Number9 = table.Column<int>(type: "integer", nullable: false),
                    Number10 = table.Column<int>(type: "integer", nullable: false),
                    Number11 = table.Column<int>(type: "integer", nullable: false),
                    Number12 = table.Column<int>(type: "integer", nullable: false),
                    Number13 = table.Column<int>(type: "integer", nullable: false),
                    Number14 = table.Column<int>(type: "integer", nullable: false),
                    Number15 = table.Column<int>(type: "integer", nullable: false),
                    Number16 = table.Column<int>(type: "integer", nullable: false),
                    Number17 = table.Column<int>(type: "integer", nullable: false),
                    Number18 = table.Column<int>(type: "integer", nullable: false),
                    Number19 = table.Column<int>(type: "integer", nullable: false),
                    Number20 = table.Column<int>(type: "integer", nullable: false),
                    Number21 = table.Column<int>(type: "integer", nullable: false),
                    Number22 = table.Column<int>(type: "integer", nullable: false),
                    Number23 = table.Column<int>(type: "integer", nullable: false),
                    Number24 = table.Column<int>(type: "integer", nullable: false),
                    Number25 = table.Column<int>(type: "integer", nullable: false),
                    Number26 = table.Column<int>(type: "integer", nullable: false),
                    Number27 = table.Column<int>(type: "integer", nullable: false),
                    Number28 = table.Column<int>(type: "integer", nullable: false),
                    Number29 = table.Column<int>(type: "integer", nullable: false),
                    Number30 = table.Column<int>(type: "integer", nullable: false),
                    Number31 = table.Column<int>(type: "integer", nullable: false),
                    Number32 = table.Column<int>(type: "integer", nullable: false),
                    Number33 = table.Column<int>(type: "integer", nullable: false),
                    Number34 = table.Column<int>(type: "integer", nullable: false),
                    Number35 = table.Column<int>(type: "integer", nullable: false),
                    Number36 = table.Column<int>(type: "integer", nullable: false),
                    Number37 = table.Column<int>(type: "integer", nullable: false),
                    Number38 = table.Column<int>(type: "integer", nullable: false),
                    Number39 = table.Column<int>(type: "integer", nullable: false),
                    Number40 = table.Column<int>(type: "integer", nullable: false),
                    Number41 = table.Column<int>(type: "integer", nullable: false),
                    Number42 = table.Column<int>(type: "integer", nullable: false),
                    Number43 = table.Column<int>(type: "integer", nullable: false),
                    Number44 = table.Column<int>(type: "integer", nullable: false),
                    Number45 = table.Column<int>(type: "integer", nullable: false),
                    Number46 = table.Column<int>(type: "integer", nullable: false),
                    Number47 = table.Column<int>(type: "integer", nullable: false),
                    Number48 = table.Column<int>(type: "integer", nullable: false),
                    Number49 = table.Column<int>(type: "integer", nullable: false),
                    Number50 = table.Column<int>(type: "integer", nullable: false),
                    Number51 = table.Column<int>(type: "integer", nullable: false),
                    Number52 = table.Column<int>(type: "integer", nullable: false),
                    Number53 = table.Column<int>(type: "integer", nullable: false),
                    Number54 = table.Column<int>(type: "integer", nullable: false),
                    Number55 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VnPowerResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VnPowerResults_VnPowers_Id",
                        column: x => x.Id,
                        principalSchema: "Game",
                        principalTable: "VnPowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VnPowerForcasts",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "VnPowerResults",
                schema: "Game");
        }
    }
}
