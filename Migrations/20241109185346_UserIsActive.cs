using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class UserIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 9, 19, 53, 46, 330, DateTimeKind.Local).AddTicks(4825), new DateTime(2024, 11, 9, 19, 53, 46, 330, DateTimeKind.Local).AddTicks(4823) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "IsActive" },
                values: new object[] { new DateTime(2024, 11, 9, 19, 53, 46, 330, DateTimeKind.Local).AddTicks(4553), true });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateOfBirth", "IsActive" },
                values: new object[] { new DateTime(2024, 11, 9, 19, 53, 46, 330, DateTimeKind.Local).AddTicks(4713), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "User");

            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 9, 14, 44, 9, 392, DateTimeKind.Local).AddTicks(6126), new DateTime(2024, 11, 9, 14, 44, 9, 392, DateTimeKind.Local).AddTicks(6123) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 9, 14, 44, 9, 392, DateTimeKind.Local).AddTicks(6010));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 9, 14, 44, 9, 392, DateTimeKind.Local).AddTicks(6061));
        }
    }
}
