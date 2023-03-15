using System;
using CarPool.DataLayer;
using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarPool.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CarPoolDataLayerCarPoolContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "OfferedRide",
                columns: table => new
                {
                    RideId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedRide", x => x.RideId);
                    table.ForeignKey(
                        name: "FK_OfferedRide_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookedRide",
                columns: table => new
                {
                    SlNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RideId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedRide", x => x.SlNo);
                    table.ForeignKey(
                        name: "FK_BookedRide_OfferedRide_RideId",
                        column: x => x.RideId,
                        principalTable: "OfferedRide",
                        principalColumn: "RideId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookedRide_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RideLocation",
                columns: table => new
                {
                    SerialNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SequenceNum = table.Column<int>(type: "int", nullable: false),
                    RideId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideLocation", x => x.SerialNo);
                    table.ForeignKey(
                        name: "FK_RideLocation_OfferedRide_RideId",
                        column: x => x.RideId,
                        principalTable: "OfferedRide",
                        principalColumn: "RideId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Name", "Password", "ProfileImage", "UserEmail" },
                values: new object[] { "gh1", "Nitish", "Nitish%%", "hkdhk", "nitish@132" });

            migrationBuilder.InsertData(
                table: "OfferedRide",
                columns: new[] { "RideId", "AvailableSeats", "Date", "Price", "Time", "UserId" },
                values: new object[] { "abc@123", 2, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 100.0, 1, "gh1" });

            migrationBuilder.InsertData(
                table: "BookedRide",
                columns: new[] { "SlNo", "RideId", "UserId" },
                values: new object[,]
                {
                    { 1, "abc@123", "gh1" },
                    { 2, "abc@123", "gh1" }
                });

            migrationBuilder.InsertData(
                table: "RideLocation",
                columns: new[] { "SerialNo", "Location", "RideId", "SequenceNum" },
                values: new object[,]
                {
                    { 1, "Delhi", "abc@123", 0 },
                    { 2, "Mumbai", "abc@123", 1 },
                    { 3, "Nagpur", "abc@123", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookedRide_RideId",
                table: "BookedRide",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedRide_UserId",
                table: "BookedRide",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedRide_UserId",
                table: "OfferedRide",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RideLocation_RideId",
                table: "RideLocation",
                column: "RideId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookedRide");

            migrationBuilder.DropTable(
                name: "RideLocation");

            migrationBuilder.DropTable(
                name: "OfferedRide");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
