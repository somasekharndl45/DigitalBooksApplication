using System;
using System.Collections.Generic;

namespace CommonUtility.DatabaseEntity
{
    public partial class Book
    {
        public Book()
        {
            Payments = new HashSet<Payment>();
        }

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
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Author AuthorNameNavigation { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
