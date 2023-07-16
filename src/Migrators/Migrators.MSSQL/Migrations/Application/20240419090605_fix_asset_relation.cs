using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_asset_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistorys_AssetStatuses_PreviousQualityStatusId",
                schema: "Property",
                table: "AssetHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistorys_AssetStatuses_PreviousUsingStatusId",
                schema: "Property",
                table: "AssetHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistorys_AssetStatuses_QualityStatusId",
                schema: "Property",
                table: "AssetHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistorys_AssetStatuses_UsingStatusId",
                schema: "Property",
                table: "AssetHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetStatuses_QualityStatusId",
                schema: "Property",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetStatuses_UsingStatusId",
                schema: "Property",
                table: "Assets");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistorys_Dimensions_PreviousQualityStatusId",
                schema: "Property",
                table: "AssetHistorys",
                column: "PreviousQualityStatusId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistorys_Dimensions_PreviousUsingStatusId",
                schema: "Property",
                table: "AssetHistorys",
                column: "PreviousUsingStatusId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistorys_Dimensions_QualityStatusId",
                schema: "Property",
                table: "AssetHistorys",
                column: "QualityStatusId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistorys_Dimensions_UsingStatusId",
                schema: "Property",
                table: "AssetHistorys",
                column: "UsingStatusId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Dimensions_QualityStatusId",
                schema: "Property",
                table: "Assets",
                column: "QualityStatusId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Dimensions_UsingStatusId",
                schema: "Property",
                table: "Assets",
                column: "UsingStatusId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistorys_Dimensions_PreviousQualityStatusId",
                schema: "Property",
                table: "AssetHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistorys_Dimensions_PreviousUsingStatusId",
                schema: "Property",
                table: "AssetHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistorys_Dimensions_QualityStatusId",
                schema: "Property",
                table: "AssetHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistorys_Dimensions_UsingStatusId",
                schema: "Property",
                table: "AssetHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Dimensions_QualityStatusId",
                schema: "Property",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Dimensions_UsingStatusId",
                schema: "Property",
                table: "Assets");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistorys_AssetStatuses_PreviousQualityStatusId",
                schema: "Property",
                table: "AssetHistorys",
                column: "PreviousQualityStatusId",
                principalSchema: "Property",
                principalTable: "AssetStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistorys_AssetStatuses_PreviousUsingStatusId",
                schema: "Property",
                table: "AssetHistorys",
                column: "PreviousUsingStatusId",
                principalSchema: "Property",
                principalTable: "AssetStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistorys_AssetStatuses_QualityStatusId",
                schema: "Property",
                table: "AssetHistorys",
                column: "QualityStatusId",
                principalSchema: "Property",
                principalTable: "AssetStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistorys_AssetStatuses_UsingStatusId",
                schema: "Property",
                table: "AssetHistorys",
                column: "UsingStatusId",
                principalSchema: "Property",
                principalTable: "AssetStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetStatuses_QualityStatusId",
                schema: "Property",
                table: "Assets",
                column: "QualityStatusId",
                principalSchema: "Property",
                principalTable: "AssetStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetStatuses_UsingStatusId",
                schema: "Property",
                table: "Assets",
                column: "UsingStatusId",
                principalSchema: "Property",
                principalTable: "AssetStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
