using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personal_trainer_api.Migrations
{
    /// <inheritdoc />
    public partial class DOB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Clients",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Clients");
        }
    }
}
