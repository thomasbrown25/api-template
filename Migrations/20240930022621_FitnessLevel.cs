using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personal_trainer_api.Migrations
{
    /// <inheritdoc />
    public partial class FitnessLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FitnessLevel",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FitnessLevel",
                table: "Clients");
        }
    }
}
