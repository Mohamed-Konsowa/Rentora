using Microsoft.EntityFrameworkCore.Migrations;
using Rentora.Domain.Models;

#nullable disable

namespace Rentora.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class seed4maincategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "Description" },
                values: new object[] { 1, "Sports", "All sports-related products" }
            );

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "Description" },
                values: new object[] { 2, "Transportations", "Vehicles and transport means" }
            );

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "Description" },
                values: new object[] { 3, "Travels", "Travel and tourism products" }
            );

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "Description" },
                values: new object[] { 4, "Electronics", "Electronic devices and accessories" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Categories where CategoryId <= 4");
        }
    }
}
