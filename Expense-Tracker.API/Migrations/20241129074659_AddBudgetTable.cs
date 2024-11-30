using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBudgetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                    table.ForeignKey(
                        name: "FK_Budgets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("635958b5-d605-4fef-8cae-6916fe7d162c"),
                column: "Date",
                value: new DateTime(2024, 11, 29, 8, 46, 59, 13, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("7ef757a7-4494-4a52-a6d0-ec8c6b823910"),
                column: "Date",
                value: new DateTime(2024, 11, 29, 8, 46, 59, 13, DateTimeKind.Local).AddTicks(3194));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("f57449ae-c365-4b0d-82f8-954c1dd562d7"),
                column: "Date",
                value: new DateTime(2024, 11, 29, 8, 46, 59, 13, DateTimeKind.Local).AddTicks(3176));

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("635958b5-d605-4fef-8cae-6916fe7d162c"),
                column: "Date",
                value: new DateTime(2024, 11, 18, 22, 1, 56, 716, DateTimeKind.Local).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("7ef757a7-4494-4a52-a6d0-ec8c6b823910"),
                column: "Date",
                value: new DateTime(2024, 11, 18, 22, 1, 56, 716, DateTimeKind.Local).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("f57449ae-c365-4b0d-82f8-954c1dd562d7"),
                column: "Date",
                value: new DateTime(2024, 11, 18, 22, 1, 56, 716, DateTimeKind.Local).AddTicks(2796));
        }
    }
}
