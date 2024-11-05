using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracker.Shared.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoreUsers",
                columns: table => new
                {
                    CoreUserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version = table.Column<string>(type: "BLOB", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreUsers", x => x.CoreUserId);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRate",
                columns: table => new
                {
                    CurrencyRateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FromCurrency = table.Column<int>(type: "INTEGER", nullable: false),
                    ToCurrency = table.Column<int>(type: "INTEGER", nullable: false),
                    Rate = table.Column<double>(type: "REAL", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "BLOB", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRate", x => x.CurrencyRateId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "BLOB", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RecurringPayment",
                columns: table => new
                {
                    RecurringPaymentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Start = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    End = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    PaymentTemplateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoreUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version = table.Column<string>(type: "BLOB", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringPayment", x => x.RecurringPaymentId);
                    table.ForeignKey(
                        name: "FK_RecurringPayment_CoreUsers_CoreUserId",
                        column: x => x.CoreUserId,
                        principalTable: "CoreUsers",
                        principalColumn: "CoreUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    RecurringPaymentId = table.Column<int>(type: "INTEGER", nullable: true),
                    CoreUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Currency = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version = table.Column<string>(type: "BLOB", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_CoreUsers_CoreUserId",
                        column: x => x.CoreUserId,
                        principalTable: "CoreUsers",
                        principalColumn: "CoreUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_RecurringPayment_RecurringPaymentId",
                        column: x => x.RecurringPaymentId,
                        principalTable: "RecurringPayment",
                        principalColumn: "RecurringPaymentId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentTemplate",
                columns: table => new
                {
                    PaymentTemplateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Currency = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecurringPaymentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version = table.Column<string>(type: "BLOB", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTemplate", x => x.PaymentTemplateId);
                    table.ForeignKey(
                        name: "FK_PaymentTemplate_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTemplate_RecurringPayment_RecurringPaymentId",
                        column: x => x.RecurringPaymentId,
                        principalTable: "RecurringPayment",
                        principalColumn: "RecurringPaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CoreUserId",
                table: "Payment",
                column: "CoreUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentTypeId",
                table: "Payment",
                column: "PaymentTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RecurringPaymentId",
                table: "Payment",
                column: "RecurringPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTemplate_PaymentTypeId",
                table: "PaymentTemplate",
                column: "PaymentTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTemplate_RecurringPaymentId",
                table: "PaymentTemplate",
                column: "RecurringPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_CoreUserId",
                table: "RecurringPayment",
                column: "CoreUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRate");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentTemplate");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "RecurringPayment");

            migrationBuilder.DropTable(
                name: "CoreUsers");
        }
    }
}
