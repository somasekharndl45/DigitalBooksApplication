namespace CommonUtilities.Model
{
    public class BookTable
    {
        public long BookId { get; set; }
        public byte[]? Logo { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public string AuthorName { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        public string Content { get; set; } = null!;
        public bool Active { get; set; }
    }
}
