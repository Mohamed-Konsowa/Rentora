using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentoraAPI.Migrations
{
    /// <inheritdoc />
    public partial class addsomecolstouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLog_AspNetUsers_ApplicationUserId",
                table: "ActivityLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_AspNetUsers_ApplicationUserId",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Product_ProductId",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_ApplicationUserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_ApplicationUserId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Product_ProductId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalCart_AspNetUsers_ApplicationUserId",
                table: "RentalCart");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalCart_Product_ProductId",
                table: "RentalCart");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_AspNetUsers_ReportedUserId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_AspNetUsers_ReporterUserId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Product_ProductId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_ApplicationUserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_AspNetUsers_FromUserId",
                table: "TransactionHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_AspNetUsers_ToUserId",
                table: "TransactionHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionHistory",
                table: "TransactionHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalCart",
                table: "RentalCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rental",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLog",
                table: "ActivityLog");

            migrationBuilder.RenameTable(
                name: "TransactionHistory",
                newName: "TransactionHistories");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameTable(
                name: "RentalCart",
                newName: "RentalCarts");

            migrationBuilder.RenameTable(
                name: "Rental",
                newName: "Rentals");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "Favorite",
                newName: "Favorites");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "ActivityLog",
                newName: "ActivityLogs");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionHistory_ToUserId",
                table: "TransactionHistories",
                newName: "IX_TransactionHistories_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionHistory_FromUserId",
                table: "TransactionHistories",
                newName: "IX_TransactionHistories_FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_ProductId",
                table: "Reviews",
                newName: "IX_Reviews_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_ApplicationUserId",
                table: "Reviews",
                newName: "IX_Reviews_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_ReporterUserId",
                table: "Reports",
                newName: "IX_Reports_ReporterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_ReportedUserId",
                table: "Reports",
                newName: "IX_Reports_ReportedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_ProductId",
                table: "Reports",
                newName: "IX_Reports_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalCart_ProductId",
                table: "RentalCarts",
                newName: "IX_RentalCarts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalCart_ApplicationUserId",
                table: "RentalCarts",
                newName: "IX_RentalCarts_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_ProductId",
                table: "Rentals",
                newName: "IX_Rentals_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_ApplicationUserId",
                table: "Rentals",
                newName: "IX_Rentals_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_ApplicationUserId",
                table: "Notifications",
                newName: "IX_Notifications_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_ProductId",
                table: "Favorites",
                newName: "IX_Favorites_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_ApplicationUserId",
                table: "Favorites",
                newName: "IX_Favorites_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityLog_ApplicationUserId",
                table: "ActivityLogs",
                newName: "IX_ActivityLogs_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailedLocation",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Governorate",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalID",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionHistories",
                table: "TransactionHistories",
                column: "TransactionHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "ReportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalCarts",
                table: "RentalCarts",
                column: "RentalCartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals",
                column: "RentalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "FavoriteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs",
                column: "ActivityLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_ApplicationUserId",
                table: "ActivityLogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_ApplicationUserId",
                table: "Favorites",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Product_ProductId",
                table: "Favorites",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ApplicationUserId",
                table: "Notifications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalCarts_AspNetUsers_ApplicationUserId",
                table: "RentalCarts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalCarts_Product_ProductId",
                table: "RentalCarts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_AspNetUsers_ApplicationUserId",
                table: "Rentals",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Product_ProductId",
                table: "Rentals",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_ReportedUserId",
                table: "Reports",
                column: "ReportedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_ReporterUserId",
                table: "Reports",
                column: "ReporterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Product_ProductId",
                table: "Reports",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ApplicationUserId",
                table: "Reviews",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Product_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistories_AspNetUsers_FromUserId",
                table: "TransactionHistories",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistories_AspNetUsers_ToUserId",
                table: "TransactionHistories",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_ApplicationUserId",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_ApplicationUserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Product_ProductId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ApplicationUserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalCarts_AspNetUsers_ApplicationUserId",
                table: "RentalCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalCarts_Product_ProductId",
                table: "RentalCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_AspNetUsers_ApplicationUserId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Product_ProductId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_ReportedUserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_ReporterUserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Product_ProductId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ApplicationUserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Product_ProductId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistories_AspNetUsers_FromUserId",
                table: "TransactionHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistories_AspNetUsers_ToUserId",
                table: "TransactionHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionHistories",
                table: "TransactionHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalCarts",
                table: "RentalCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DetailedLocation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Governorate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IDImage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NationalID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TransactionHistories",
                newName: "TransactionHistory");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameTable(
                name: "Rentals",
                newName: "Rental");

            migrationBuilder.RenameTable(
                name: "RentalCarts",
                newName: "RentalCart");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "Favorite");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "ActivityLogs",
                newName: "ActivityLog");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionHistories_ToUserId",
                table: "TransactionHistory",
                newName: "IX_TransactionHistory_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionHistories_FromUserId",
                table: "TransactionHistory",
                newName: "IX_TransactionHistory_FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductId",
                table: "Review",
                newName: "IX_Review_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ApplicationUserId",
                table: "Review",
                newName: "IX_Review_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ReporterUserId",
                table: "Report",
                newName: "IX_Report_ReporterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ReportedUserId",
                table: "Report",
                newName: "IX_Report_ReportedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ProductId",
                table: "Report",
                newName: "IX_Report_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_ProductId",
                table: "Rental",
                newName: "IX_Rental_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_ApplicationUserId",
                table: "Rental",
                newName: "IX_Rental_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalCarts_ProductId",
                table: "RentalCart",
                newName: "IX_RentalCart_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalCarts_ApplicationUserId",
                table: "RentalCart",
                newName: "IX_RentalCart_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ApplicationUserId",
                table: "Notification",
                newName: "IX_Notification_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorite",
                newName: "IX_Favorite_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_ApplicationUserId",
                table: "Favorite",
                newName: "IX_Favorite_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityLogs_ApplicationUserId",
                table: "ActivityLog",
                newName: "IX_ActivityLog_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionHistory",
                table: "TransactionHistory",
                column: "TransactionHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "ReportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rental",
                table: "Rental",
                column: "RentalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalCart",
                table: "RentalCart",
                column: "RentalCartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite",
                column: "FavoriteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLog",
                table: "ActivityLog",
                column: "ActivityLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLog_AspNetUsers_ApplicationUserId",
                table: "ActivityLog",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_AspNetUsers_ApplicationUserId",
                table: "Favorite",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Product_ProductId",
                table: "Favorite",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_ApplicationUserId",
                table: "Notification",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_AspNetUsers_ApplicationUserId",
                table: "Rental",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Product_ProductId",
                table: "Rental",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalCart_AspNetUsers_ApplicationUserId",
                table: "RentalCart",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalCart_Product_ProductId",
                table: "RentalCart",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_AspNetUsers_ReportedUserId",
                table: "Report",
                column: "ReportedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_AspNetUsers_ReporterUserId",
                table: "Report",
                column: "ReporterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Product_ProductId",
                table: "Report",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_ApplicationUserId",
                table: "Review",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_AspNetUsers_FromUserId",
                table: "TransactionHistory",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_AspNetUsers_ToUserId",
                table: "TransactionHistory",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
