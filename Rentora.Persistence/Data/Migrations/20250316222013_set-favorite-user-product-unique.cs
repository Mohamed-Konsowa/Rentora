using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentora.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class setfavoriteuserproductunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorites_ApplicationUserId",
                table: "Favorites");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ApplicationUserId_ProductId",
                table: "Favorites",
                columns: new[] { "ApplicationUserId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorites_ApplicationUserId_ProductId",
                table: "Favorites");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ApplicationUserId",
                table: "Favorites",
                column: "ApplicationUserId");
        }
    }
}
