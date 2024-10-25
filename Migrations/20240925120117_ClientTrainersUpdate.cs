using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personal_trainer_api.Migrations
{
    /// <inheritdoc />
    public partial class ClientTrainersUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Clients",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Clients");
        }
    }
}
