using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentoraAPI.Migrations
{
    /// <inheritdoc />
    public partial class solvingprobleminrentalcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalCart_AspNetUsers_ApplicationUserId1",
                table: "RentalCart");

            migrationBuilder.DropIndex(
                name: "IX_RentalCart_ApplicationUserId1",
                table: "RentalCart");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "RentalCart");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "RentalCart",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RentalCart_ApplicationUserId",
                table: "RentalCart",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalCart_AspNetUsers_ApplicationUserId",
                table: "RentalCart",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalCart_AspNetUsers_ApplicationUserId",
                table: "RentalCart");

            migrationBuilder.DropIndex(
                name: "IX_RentalCart_ApplicationUserId",
                table: "RentalCart");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "RentalCart",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "RentalCart",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RentalCart_ApplicationUserId1",
                table: "RentalCart",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalCart_AspNetUsers_ApplicationUserId1",
                table: "RentalCart",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
