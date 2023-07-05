using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelABC_API.Migrations
{
    /// <inheritdoc />
    public partial class again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Rooms_RoomId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RoomId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RelativeRelationId",
                table: "Images",
                column: "RelativeRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Rooms_RelativeRelationId",
                table: "Images",
                column: "RelativeRelationId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Rooms_RelativeRelationId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RelativeRelationId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Images_RoomId",
                table: "Images",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Rooms_RoomId",
                table: "Images",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
