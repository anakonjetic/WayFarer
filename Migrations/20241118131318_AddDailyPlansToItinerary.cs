using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class AddDailyPlansToItinerary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DailyPlans",
                table: "Itinerary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 13, 18, 244, DateTimeKind.Local).AddTicks(3326), new DateTime(2024, 11, 18, 14, 13, 18, 244, DateTimeKind.Local).AddTicks(3324) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 18, 14, 13, 18, 244, DateTimeKind.Local).AddTicks(3209));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 18, 14, 13, 18, 244, DateTimeKind.Local).AddTicks(3267));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyPlans",
                table: "Itinerary");

            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 17, 22, 27, 1, 699, DateTimeKind.Local).AddTicks(8679), new DateTime(2024, 11, 17, 22, 27, 1, 699, DateTimeKind.Local).AddTicks(8676) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 17, 22, 27, 1, 699, DateTimeKind.Local).AddTicks(8447));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 17, 22, 27, 1, 699, DateTimeKind.Local).AddTicks(8578));
        }
    }
}
