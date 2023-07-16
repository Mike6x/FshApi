using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fix_quiz_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizMode",
                schema: "Elearning",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                schema: "Elearning",
                table: "Quizs",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                schema: "Elearning",
                table: "Quizs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                schema: "Elearning",
                table: "QuizResults",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizMode",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "Elearning",
                table: "QuizResults");
        }
    }
}
