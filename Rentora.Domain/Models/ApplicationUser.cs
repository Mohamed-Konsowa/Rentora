using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        public string? NationalID {  get; set; }
        public string? IDImage { get; set; }
        public string? Address { get; set; }
        public string? Governorate { get; set; }
        public string? DetailedLocation { get; set; }
        public decimal? Balance { get; set; }
        public string? ProfileImage { get; set; }


        public List<Product> Products { get; set; }
        public List<Rental> Rentals { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Favorite> Favorites { get; set; }
        public List<Review> Reviews { get; set; }
        public List<RentalCart> RentalCarts { get; set; }
        public List<ActivityLog> ActivityLogs { get; set; }
        public List<Report> Reports { get; set; }
        public List<Report> Reporteds { get; set; }
        public List<TransactionHistory> ToTransactionHistories { get; set; }
        public List<TransactionHistory> FromTransactionHistories { get; set; }

    }
}
