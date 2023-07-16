using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_message_user_relation_ship_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_FromUserId",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_ToUserId",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.AlterColumn<string>(
                name: "ToUserId",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_FromUserId",
                schema: "Communication",
                table: "ChatMessages",
                column: "FromUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_ToUserId",
                schema: "Communication",
                table: "ChatMessages",
                column: "ToUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_FromUserId",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_ToUserId",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.AlterColumn<string>(
                name: "ToUserId",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                schema: "Communication",
                table: "ChatMessages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_FromUserId",
                schema: "Communication",
                table: "ChatMessages",
                column: "FromUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_ToUserId",
                schema: "Communication",
                table: "ChatMessages",
                column: "ToUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
