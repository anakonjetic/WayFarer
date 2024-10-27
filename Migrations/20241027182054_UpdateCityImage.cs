using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class UpdateCityImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "City",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://wallpapercave.com/wp/wp2333635.jpg");

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 2, "Pariz je grad ljubavi!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT79lMmLbkyF2Dj2u1pNmWrjlUZfAjDQak0VA&s", "Pariz" },
                    { 3, "Gospić je najveći grad u Europi!", "https://www.mare-vrbnik.com/public/uploads/photos/articles/_gospic.jpg", "Gospić" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Image",
                table: "City");

            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 27, 18, 47, 39, 469, DateTimeKind.Local).AddTicks(7284), new DateTime(2024, 10, 27, 18, 47, 39, 469, DateTimeKind.Local).AddTicks(7282) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 10, 27, 18, 47, 39, 469, DateTimeKind.Local).AddTicks(7077));
        }
    }
}
