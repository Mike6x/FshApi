using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
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
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                schema: "Elearning",
                table: "Quizs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                schema: "Elearning",
                table: "Quizs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                schema: "Elearning",
                table: "QuizResults",
                type: "decimal(18,2)",
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
