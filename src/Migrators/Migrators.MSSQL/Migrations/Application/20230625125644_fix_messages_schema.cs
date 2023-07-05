using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_messages_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_FromUserId",
                schema: "Catalog",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_ToUserId",
                schema: "Catalog",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_FromUserId",
                schema: "Catalog",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ToUserId",
                schema: "Catalog",
                table: "ChatMessages");

            migrationBuilder.EnsureSchema(
                name: "Communication");

            migrationBuilder.RenameTable(
                name: "ChatMessages",
                schema: "Catalog",
                newName: "ChatMessages",
                newSchema: "Communication");

            migrationBuilder.AlterColumn<string>(
                name: "ToUserId",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FromUserFirstName",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FromUserImageUrl",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromUserLastName",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromUserName",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToUserFirstName",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToUserImageUrl",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToUserLastName",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToUserName",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromUserFirstName",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "FromUserImageUrl",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "FromUserLastName",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "FromUserName",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "ToUserFirstName",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "ToUserImageUrl",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "ToUserLastName",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "ToUserName",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "ChatMessages",
                schema: "Communication",
                newName: "ChatMessages",
                newSchema: "Catalog");

            migrationBuilder.AlterColumn<string>(
                name: "ToUserId",
                schema: "Catalog",
                table: "ChatMessages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                schema: "Catalog",
                table: "ChatMessages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_FromUserId",
                schema: "Catalog",
                table: "ChatMessages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ToUserId",
                schema: "Catalog",
                table: "ChatMessages",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_FromUserId",
                schema: "Catalog",
                table: "ChatMessages",
                column: "FromUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_ToUserId",
                schema: "Catalog",
                table: "ChatMessages",
                column: "ToUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
