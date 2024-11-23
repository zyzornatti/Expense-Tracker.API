using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Expense_Tracker.API.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { new Guid("5a5f8530-846c-4398-a264-1e6cabdd053e"), "zaltego@etracker.com", "Oluwatobi123", "zaltego" },
                    { new Guid("b3c5814b-9434-4520-9887-64d1979db9b0"), "zyzornatti@etracker.com", "Oluwatobi123", "zyzornatti" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("4df523f6-5e73-43af-aaf1-0c980e16fc9f"), "Food", new Guid("b3c5814b-9434-4520-9887-64d1979db9b0") },
                    { new Guid("67ecebc9-e619-4f84-97a4-f33f5b59790a"), "Transport", new Guid("b3c5814b-9434-4520-9887-64d1979db9b0") },
                    { new Guid("9d86390b-8e1b-4b82-baff-c0b953f1ef1e"), "Utilities", new Guid("5a5f8530-846c-4398-a264-1e6cabdd053e") }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseId", "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[,]
                {
                    { new Guid("635958b5-d605-4fef-8cae-6916fe7d162c"), 120.00m, new Guid("9d86390b-8e1b-4b82-baff-c0b953f1ef1e"), new DateTime(2024, 11, 18, 22, 1, 56, 716, DateTimeKind.Local).AddTicks(2820), "Electric Bill", new Guid("5a5f8530-846c-4398-a264-1e6cabdd053e") },
                    { new Guid("7ef757a7-4494-4a52-a6d0-ec8c6b823910"), 15.00m, new Guid("67ecebc9-e619-4f84-97a4-f33f5b59790a"), new DateTime(2024, 11, 18, 22, 1, 56, 716, DateTimeKind.Local).AddTicks(2815), "Bus Ticket", new Guid("b3c5814b-9434-4520-9887-64d1979db9b0") },
                    { new Guid("f57449ae-c365-4b0d-82f8-954c1dd562d7"), 50.75m, new Guid("4df523f6-5e73-43af-aaf1-0c980e16fc9f"), new DateTime(2024, 11, 18, 22, 1, 56, 716, DateTimeKind.Local).AddTicks(2796), "Groceries", new Guid("b3c5814b-9434-4520-9887-64d1979db9b0") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("635958b5-d605-4fef-8cae-6916fe7d162c"));

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("7ef757a7-4494-4a52-a6d0-ec8c6b823910"));

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: new Guid("f57449ae-c365-4b0d-82f8-954c1dd562d7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("4df523f6-5e73-43af-aaf1-0c980e16fc9f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("67ecebc9-e619-4f84-97a4-f33f5b59790a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("9d86390b-8e1b-4b82-baff-c0b953f1ef1e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5a5f8530-846c-4398-a264-1e6cabdd053e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b3c5814b-9434-4520-9887-64d1979db9b0"));
        }
    }
}
