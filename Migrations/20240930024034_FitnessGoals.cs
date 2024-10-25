using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace template_api.Migrations
{
    /// <inheritdoc />
    public partial class FitnessGoals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FitnessGoals",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WeightGoal",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FitnessGoals",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "WeightGoal",
                table: "Clients");
        }
    }
}
