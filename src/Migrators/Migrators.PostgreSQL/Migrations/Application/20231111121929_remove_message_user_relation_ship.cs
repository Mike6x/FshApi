using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class remove_message_user_relation_ship : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_FromUserId",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ToUserId",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                schema: "Communication",
                table: "ChatMessages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImageMessage",
                schema: "Communication",
                table: "ChatMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "IsImageMessage",
                schema: "Communication",
                table: "ChatMessages");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_FromUserId",
                schema: "Communication",
                table: "ChatMessages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ToUserId",
                schema: "Communication",
                table: "ChatMessages",
                column: "ToUserId");

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
    }
}
