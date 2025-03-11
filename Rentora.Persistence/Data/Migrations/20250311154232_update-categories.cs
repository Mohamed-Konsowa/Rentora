using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentora.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatecategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body_Type",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "Fuel_Type",
                table: "Travels");

            migrationBuilder.RenameColumn(
                name: "Transmission",
                table: "Travels",
                newName: "Condition");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Transportations",
                newName: "Transmission");

            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "Product",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Body_Type",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fuel_Type",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RentalPeriod",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body_Type",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Fuel_Type",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "RentalPeriod",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Travels",
                newName: "Transmission");

            migrationBuilder.RenameColumn(
                name: "Transmission",
                table: "Transportations",
                newName: "Condition");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "PricePerDay");

            migrationBuilder.AddColumn<string>(
                name: "Body_Type",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fuel_Type",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
