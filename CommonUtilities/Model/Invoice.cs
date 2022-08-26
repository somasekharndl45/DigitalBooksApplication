namespace CommonUtilities.Model
{
    public class Invoice
    {
        public int PaymentId { get; set; }
        public string BuyerEmailId { get; set; } = null!;
        public string BuyerName { get; set; } = null!;
        public string BookName { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
    }
}
