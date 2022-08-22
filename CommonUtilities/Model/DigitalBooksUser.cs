using System;
using System.Collections.Generic;

namespace CommonUtilities.Model
{
    public partial class DigitalBooksUser
    {
        public DigitalBooksUser()
        {
            Books = new HashSet<Book>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserPass { get; set; } = null!;
        public string UserRole { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
