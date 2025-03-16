using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentora.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class setcartuserproductunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentalCarts_ApplicationUserId",
                table: "RentalCarts");

            migrationBuilder.CreateIndex(
                name: "IX_RentalCarts_ApplicationUserId_ProductId",
                table: "RentalCarts",
                columns: new[] { "ApplicationUserId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentalCarts_ApplicationUserId_ProductId",
                table: "RentalCarts");

            migrationBuilder.CreateIndex(
                name: "IX_RentalCarts_ApplicationUserId",
                table: "RentalCarts",
                column: "ApplicationUserId");
        }
    }
}
