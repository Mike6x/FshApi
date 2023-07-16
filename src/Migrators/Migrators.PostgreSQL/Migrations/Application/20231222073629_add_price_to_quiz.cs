using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class add_price_to_quiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Elearning",
                table: "Quizs",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sale",
                schema: "Elearning",
                table: "Quizs",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Elearning",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "Sale",
                schema: "Elearning",
                table: "Quizs");
        }
    }
}
