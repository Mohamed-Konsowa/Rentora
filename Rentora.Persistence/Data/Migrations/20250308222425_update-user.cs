using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentora.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDImage",
                table: "AspNetUsers",
                newName: "Town");

            migrationBuilder.RenameColumn(
                name: "DetailedLocation",
                table: "AspNetUsers",
                newName: "Personal_summary");

            migrationBuilder.AddColumn<string>(
                name: "IDImageBack",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDImageFront",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDImageBack",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IDImageFront",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Town",
                table: "AspNetUsers",
                newName: "IDImage");

            migrationBuilder.RenameColumn(
                name: "Personal_summary",
                table: "AspNetUsers",
                newName: "DetailedLocation");
        }
    }
}
