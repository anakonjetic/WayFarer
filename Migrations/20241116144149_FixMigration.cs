using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFarer.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Gender = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attraction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attraction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attraction_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itinerary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itinerary_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    AttractionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Attraction_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Zagreb je najljepši grad!", "https://wallpapercave.com/wp/wp2333635.jpg", "Zagreb" },
                    { 2, "Pariz je grad ljubavi!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT79lMmLbkyF2Dj2u1pNmWrjlUZfAjDQak0VA&s", "Pariz" },
                    { 3, "Gospić je najveći grad u Europi!", "https://www.mare-vrbnik.com/public/uploads/photos/articles/_gospic.jpg", "Gospić" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "AttractionId", "CityId", "Comment", "Rating", "UserId" },
                values: new object[] { 1, 0, 0, "Najbolje mjesto u gradu, uživali smo u noći pjesnika!", 5, 0 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateOfBirth", "Email", "Gender", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 16, 15, 41, 49, 313, DateTimeKind.Local).AddTicks(235), "skorkut@gmail.com", 0, true, "Srećko", "divasGusteglata", 0, "Korkut", "caslavBenzoni" },
                    { 2, new DateTime(2024, 11, 16, 15, 41, 49, 313, DateTimeKind.Local).AddTicks(299), "ignacijefuchs@gmail.com", 0, true, "Vatroslav", "goriArena123", 1, "Lisinski", "ignacijeFux" }
                });

            migrationBuilder.InsertData(
                table: "Attraction",
                columns: new[] { "Id", "Category", "CityId", "Image", "Name", "Price" },
                values: new object[] { 1, 4, 1, null, "Bitange i princeze", 7m });

            migrationBuilder.InsertData(
                table: "Itinerary",
                columns: new[] { "Id", "CityId", "EndDate", "StartDate", "TotalPrice", "UserId" },
                values: new object[] { 1, 1, new DateTime(2024, 11, 16, 15, 41, 49, 313, DateTimeKind.Local).AddTicks(408), new DateTime(2024, 11, 16, 15, 41, 49, 313, DateTimeKind.Local).AddTicks(406), 0m, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Attraction_CityId",
                table: "Attraction",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerary_CityId",
                table: "Itinerary",
                column: "CityId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itinerary");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Attraction");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
