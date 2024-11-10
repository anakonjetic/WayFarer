using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class UpdateAttributeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the old foreign key before renaming columns
            migrationBuilder.DropForeignKey(
                name: "FK_Attraction_City_cityId",
                table: "Attraction");

            // Rename the columns as per the new naming convention
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Review",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "cityId",
                table: "Review",
                newName: "CityId");

            migrationBuilder.RenameColumn(
                name: "attractionId",
                table: "Review",
                newName: "AttractionId");

            migrationBuilder.RenameColumn(
                name: "cityId",
                table: "Attraction",
                newName: "CityId");

            // Rename the indexes to match the new column names
            migrationBuilder.RenameIndex(
                name: "IX_Attraction_cityId",
                table: "Attraction",
                newName: "IX_Attraction_CityId");

            // Update data in the tables (dates are updated as an example)
            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 25, 49, 597, DateTimeKind.Local).AddTicks(8241), new DateTime(2024, 11, 10, 22, 25, 49, 597, DateTimeKind.Local).AddTicks(8238) });

            migrationBuilder.UpdateData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 10, 22, 25, 49, 597, DateTimeKind.Local).AddTicks(8043));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2024, 11, 10, 22, 25, 49, 597, DateTimeKind.Local).AddTicks(8134));

            // Create the indexes for the newly renamed columns
            migrationBuilder.CreateIndex(
                name: "IX_Review_AttractionId",
                table: "Review",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_CityId",
                table: "Review",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            // Add the foreign key constraints with ON DELETE NO ACTION to avoid cascade conflicts
            migrationBuilder.AddForeignKey(
                name: "FK_Attraction_City_CityId",
                table: "Attraction",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Prevent cascading delete here

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Attraction_AttractionId",
                table: "Review",
                column: "AttractionId",
                principalTable: "Attraction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // Cascade delete for AttractionId

            migrationBuilder.AddForeignKey(
                name: "FK_Review_City_CityId",
                table: "Review",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Prevent cascading delete here

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // Cascade delete for UserId
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign keys added in the Up migration
            migrationBuilder.DropForeignKey(
                name: "FK_Attraction_City_CityId",
                table: "Attraction");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Attraction_AttractionId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_City_CityId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_UserId",
                table: "Review");

            // Drop the indexes
            migrationBuilder.DropIndex(
                name: "IX_Review_AttractionId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_CityId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserId",
                table: "Review");

            // Rename the columns back to the original names
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Review",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Review",
                newName: "cityId");

            migrationBuilder.RenameColumn(
                name: "AttractionId",
                table: "Review",
                newName: "attractionId");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Attraction",
                newName: "cityId");

            // Rename the indexes back to the original names
            migrationBuilder.RenameIndex(
                name: "IX_Attraction_CityId",
                table: "Attraction",
                newName: "IX_Attraction_cityId");

            // Revert the updated data if needed (can be customized)
            migrationBuilder.UpdateData(
                table: "Itinerary",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 9, 14, 44, 9, 392, DateTimeKind.Local).AddTicks(6126), new DateTime(2024, 11, 9, 14, 44, 9, 392, DateTimeKind.Local).AddTicks(6123) });

            migrationBuilder.UpdateData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 1,
                column: "userId",
                value: 1);

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

            // Add the old foreign key back
            migrationBuilder.AddForeignKey(
                name: "FK_Attraction_City_cityId",
                table: "Attraction",
                column: "cityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
