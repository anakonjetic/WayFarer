using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class NewBasicUserSecondTry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 6, 21, 5, 12, 556, DateTimeKind.Local).AddTicks(2922), new DateTime(2024, 11, 6, 21, 5, 12, 556, DateTimeKind.Local).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 6, 21, 5, 12, 556, DateTimeKind.Local).AddTicks(2734));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateOfBirth", "Email", "Gender", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { 2, new DateTime(2024, 11, 6, 21, 5, 12, 556, DateTimeKind.Local).AddTicks(2819), "ignacijefuchs@gmail.com", 0, "Vatroslav", "goriArena123", 1, "Lisinski", "ignacijeFux" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 6, 20, 59, 57, 74, DateTimeKind.Local).AddTicks(1616), new DateTime(2024, 11, 6, 20, 59, 57, 74, DateTimeKind.Local).AddTicks(1614) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 6, 20, 59, 57, 74, DateTimeKind.Local).AddTicks(1426));
        }
    }
}
