using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelABC_API.Migrations
{
    /// <inheritdoc />
    public partial class roomsavaible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomReservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    hoursHired = table.Column<int>(type: "int", nullable: true),
                    extraHoursHired = table.Column<int>(type: "int", nullable: true),
                    checkIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    checkOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    initialPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    totalPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomReservations_Rooms_roomId",
                        column: x => x.roomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoomReservations",
                columns: new[] { "Id", "checkIn", "checkOut", "extraHoursHired", "hoursHired", "initialPayment", "roomId", "status", "totalPayment" },
                values: new object[,]
                {
                    { new Guid("04f92dbf-b777-43d2-8dc4-992d377b0de6"), null, null, null, null, null, new Guid("72727aa7-ddc3-4844-8e79-08db827c2d2c"), null, null },
                    { new Guid("103a572d-b7d0-4b55-a0f4-dfe2f44b35c8"), null, null, null, null, null, new Guid("eb2fa93b-c9c5-4755-8e7a-08db827c2d2c"), null, null },
                    { new Guid("4803c047-9560-4a68-931c-d4ca440d9842"), null, null, null, null, null, new Guid("eb2fa93b-c9c5-4755-8e7a-08db827c2d2c"), null, null },
                    { new Guid("90637a78-cc56-43c6-85f8-e07267c0c423"), null, null, null, null, null, new Guid("72727aa7-ddc3-4844-8e79-08db827c2d2c"), null, null },
                    { new Guid("c4b26c6f-4042-48e9-8bf3-ef0d304329df"), null, null, null, null, null, new Guid("72727aa7-ddc3-4844-8e79-08db827c2d2c"), null, null },
                    { new Guid("cd481ebf-72b0-4fa6-b9bc-590370b4b486"), null, null, null, null, null, new Guid("5ec91cd3-2842-4a51-c394-08db81affe60"), null, null },
                    { new Guid("e810140b-0a82-4811-83ae-955558242ac2"), null, null, null, null, null, new Guid("eb2fa93b-c9c5-4755-8e7a-08db827c2d2c"), null, null },
                    { new Guid("f52edc5c-64a2-405d-a18b-6b0b1a29d35e"), null, null, null, null, null, new Guid("5ec91cd3-2842-4a51-c394-08db81affe60"), null, null },
                    { new Guid("f96806f4-84d8-4ba1-afb2-074debdb40d8"), null, null, null, null, null, new Guid("5ec91cd3-2842-4a51-c394-08db81affe60"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservations_roomId",
                table: "RoomReservations",
                column: "roomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomReservations");
        }
    }
}
