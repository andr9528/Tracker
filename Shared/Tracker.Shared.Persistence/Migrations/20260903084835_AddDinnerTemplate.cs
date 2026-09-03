using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracker.Shared.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDinnerTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DinnerTemplate",
                columns: table => new
                {
                    DinnerTemplateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsTakeAway = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasLeftovers = table.Column<bool>(type: "INTEGER", nullable: false),
                    LeftoversEnoughForDinner = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLeftovers = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEatenOut = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsReadyMadeDish = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "BLOB", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinnerTemplate", x => x.DinnerTemplateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DinnerTemplate_Name",
                table: "DinnerTemplate",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DinnerTemplate");
        }
    }
}
