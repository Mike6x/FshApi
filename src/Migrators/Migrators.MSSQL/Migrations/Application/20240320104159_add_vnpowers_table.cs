using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DrawId = table.Column<int>(type: "int", nullable: false),
                    DrawDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WinNumber1 = table.Column<int>(type: "int", nullable: false),
                    WinNumber2 = table.Column<int>(type: "int", nullable: false),
                    WinNumber3 = table.Column<int>(type: "int", nullable: false),
                    WinNumber4 = table.Column<int>(type: "int", nullable: false),
                    WinNumber5 = table.Column<int>(type: "int", nullable: false),
                    WinNumber6 = table.Column<int>(type: "int", nullable: false),
                    BonusNumber = table.Column<int>(type: "int", nullable: false),
                    Jackpot1 = table.Column<int>(type: "int", nullable: false),
                    Jackpot2 = table.Column<int>(type: "int", nullable: false),
                    FirstPrize = table.Column<int>(type: "int", nullable: false),
                    SecondPrize = table.Column<int>(type: "int", nullable: false),
                    ThirdPrize = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false),
                    SubRoundId = table.Column<int>(type: "int", nullable: false),
                    JackpotAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WinStr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
