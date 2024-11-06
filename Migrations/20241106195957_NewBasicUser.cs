using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class NewBasicUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 27, 19, 20, 54, 21, DateTimeKind.Local).AddTicks(8624), new DateTime(2024, 10, 27, 19, 20, 54, 21, DateTimeKind.Local).AddTicks(8621) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 10, 27, 19, 20, 54, 21, DateTimeKind.Local).AddTicks(8397));
        }
    }
}
