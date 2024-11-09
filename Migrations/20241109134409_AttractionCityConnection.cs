using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class AttractionCityConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Attraction_cityId",
                table: "Attraction",
                column: "cityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attraction_City_cityId",
                table: "Attraction",
                column: "cityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attraction_City_cityId",
                table: "Attraction");

            migrationBuilder.DropIndex(
                name: "IX_Attraction_cityId",
                table: "Attraction");

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

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 6, 21, 5, 12, 556, DateTimeKind.Local).AddTicks(2819));
        }
    }
}
