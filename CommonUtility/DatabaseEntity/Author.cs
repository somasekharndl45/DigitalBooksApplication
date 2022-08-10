using System;
using System.Collections.Generic;

namespace CommonUtility.DatabaseEntity
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public long AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public string? AuthorPassword { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
