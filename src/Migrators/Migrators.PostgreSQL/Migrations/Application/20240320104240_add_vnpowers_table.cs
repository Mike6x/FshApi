using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class add_vnpowers_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Game");

            migrationBuilder.CreateTable(
                name: "VnPowers",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DrawId = table.Column<int>(type: "integer", nullable: false),
                    DrawDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WinNumber1 = table.Column<int>(type: "integer", nullable: false),
                    WinNumber2 = table.Column<int>(type: "integer", nullable: false),
                    WinNumber3 = table.Column<int>(type: "integer", nullable: false),
                    WinNumber4 = table.Column<int>(type: "integer", nullable: false),
                    WinNumber5 = table.Column<int>(type: "integer", nullable: false),
                    WinNumber6 = table.Column<int>(type: "integer", nullable: false),
                    BonusNumber = table.Column<int>(type: "integer", nullable: false),
                    Jackpot1 = table.Column<int>(type: "integer", nullable: false),
                    Jackpot2 = table.Column<int>(type: "integer", nullable: false),
                    FirstPrize = table.Column<int>(type: "integer", nullable: false),
                    SecondPrize = table.Column<int>(type: "integer", nullable: false),
                    ThirdPrize = table.Column<int>(type: "integer", nullable: false),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    SubRoundId = table.Column<int>(type: "integer", nullable: false),
                    JackpotAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    WinStr = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_VnPowers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VnPowers",
                schema: "Game");
        }
    }
}
