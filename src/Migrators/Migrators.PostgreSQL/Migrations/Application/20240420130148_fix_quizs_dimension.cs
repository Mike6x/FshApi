using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_quizs_dimension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizMode",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "QuizTopic",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "QuizType",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.AddColumn<Guid>(
                name: "QuizModeId",
                schema: "Elearning",
                table: "Quizs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuizTopicId",
                schema: "Elearning",
                table: "Quizs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuizTypeId",
                schema: "Elearning",
                table: "Quizs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_QuizModeId",
                schema: "Elearning",
                table: "Quizs",
                column: "QuizModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_QuizTopicId",
                schema: "Elearning",
                table: "Quizs",
                column: "QuizTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_QuizTypeId",
                schema: "Elearning",
                table: "Quizs",
                column: "QuizTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizs_Dimensions_QuizModeId",
                schema: "Elearning",
                table: "Quizs",
                column: "QuizModeId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizs_Dimensions_QuizTopicId",
                schema: "Elearning",
                table: "Quizs",
                column: "QuizTopicId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizs_Dimensions_QuizTypeId",
                schema: "Elearning",
                table: "Quizs",
                column: "QuizTypeId",
                principalSchema: "Settings",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizs_Dimensions_QuizModeId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizs_Dimensions_QuizTopicId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizs_Dimensions_QuizTypeId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropIndex(
                name: "IX_Quizs_QuizModeId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropIndex(
                name: "IX_Quizs_QuizTopicId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropIndex(
                name: "IX_Quizs_QuizTypeId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "QuizModeId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "QuizTopicId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "QuizTypeId",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.AddColumn<int>(
                name: "QuizMode",
                schema: "Elearning",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizTopic",
                schema: "Elearning",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizType",
                schema: "Elearning",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
