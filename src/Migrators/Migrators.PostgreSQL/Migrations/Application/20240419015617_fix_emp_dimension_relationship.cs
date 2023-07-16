using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_emp_dimension_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Titles_TitleId",
                schema: "People",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TitleId",
                schema: "People",
                table: "Employees",
                newName: "DimensionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_TitleId",
                schema: "People",
                table: "Employees",
                newName: "IX_Employees_DimensionId");

            migrationBuilder.CreateTable(
                name: "Dimensions",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    NativeName = table.Column<string>(type: "text", nullable: true),
                    FullNativeName = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    FatherId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_Dimensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dimensions_Dimensions_FatherId",
                        column: x => x.FatherId,
                        principalSchema: "Settings",
                        principalTable: "Dimensions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dimensions_FatherId",
                schema: "Settings",
                table: "Dimensions",
                column: "FatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Dimensions_DimensionId",
                schema: "People",
                table: "Employees",
                column: "DimensionId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Dimensions_DimensionId",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Dimensions",
                schema: "Settings");

            migrationBuilder.RenameColumn(
                name: "DimensionId",
                schema: "People",
                table: "Employees",
                newName: "TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DimensionId",
                schema: "People",
                table: "Employees",
                newName: "IX_Employees_TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Titles_TitleId",
                schema: "People",
                table: "Employees",
                column: "TitleId",
                principalSchema: "People",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
