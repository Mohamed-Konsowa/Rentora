using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentora.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class somechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentalPeriod",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Product",
                newName: "ProductStatus");

            migrationBuilder.AddColumn<int>(
                name: "RentStatus",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentStatus",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "ProductStatus",
                table: "Product",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RentalPeriod",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
