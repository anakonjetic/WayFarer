using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class UpdateItineraryForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Itinerary",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "cityId",
                table: "Itinerary",
                newName: "CityId");

            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CityId", "EndDate", "StartDate" },
                values: new object[] { 1, new DateTime(2024, 10, 27, 18, 47, 39, 469, DateTimeKind.Local).AddTicks(7284), new DateTime(2024, 10, 27, 18, 47, 39, 469, DateTimeKind.Local).AddTicks(7282) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 10, 27, 18, 47, 39, 469, DateTimeKind.Local).AddTicks(7077));

            migrationBuilder.CreateIndex(
                name: "IX_Itinerary_CityId",
                table: "Itinerary",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerary_City_CityId",
                table: "Itinerary",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerary_City_CityId",
                table: "Itinerary");

            migrationBuilder.DropIndex(
                name: "IX_Itinerary_CityId",
                table: "Itinerary");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Itinerary",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Itinerary",
                newName: "cityId");

            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate", "cityId" },
                values: new object[] { new DateTime(2024, 10, 24, 2, 11, 24, 800, DateTimeKind.Local).AddTicks(5474), new DateTime(2024, 10, 24, 2, 11, 24, 800, DateTimeKind.Local).AddTicks(5472), 0 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 10, 24, 2, 11, 24, 800, DateTimeKind.Local).AddTicks(5310));
        }
    }
}
