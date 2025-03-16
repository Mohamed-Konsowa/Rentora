namespace Rentora.Application.DTOs.Rental
{
    public class RentProductDTO
    {
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime StartDate { get; set; }
        public int numOfDays {  get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal PenaltyFee { get; set; } = 0.00m;
    }
}
