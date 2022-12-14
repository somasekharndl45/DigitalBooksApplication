namespace CommonUtilities.Model
{
    public class BookInformation
    {
        public int BookId { get; set; }
        public byte[]? Logo { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string Category { get; set; } = null!;
    }
}
