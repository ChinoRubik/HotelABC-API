using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelABC_API.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Characteristics",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Rooms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Characteristics",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rooms");
        }
    }
}
