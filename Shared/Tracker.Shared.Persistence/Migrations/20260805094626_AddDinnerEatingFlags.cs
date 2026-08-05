using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracker.Shared.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDinnerEatingFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEatenOut",
                table: "Dinner",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadyMadeDish",
                table: "Dinner",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEatenOut",
                table: "Dinner");

            migrationBuilder.DropColumn(
                name: "IsReadyMadeDish",
                table: "Dinner");
        }
    }
}
