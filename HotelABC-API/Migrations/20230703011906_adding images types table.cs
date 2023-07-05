using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelABC_API.Migrations
{
    /// <inheritdoc />
    public partial class addingimagestypestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ImageTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("3897b275-7a3f-4a84-a620-105b9b0eb89a"), "room" },
                    { new Guid("8929b4bf-5be3-4002-8ad6-b9f46f782f16"), "offers" },
                    { new Guid("de63304d-8500-4570-8333-abb077e5a23f"), "food" },
                    { new Guid("e4567686-1b4d-483d-a374-9e99306c8e7b"), "carousel" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageTypes");
        }
    }
}
