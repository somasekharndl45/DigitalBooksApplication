using System;
using System.Collections.Generic;

namespace CommonUtilities.DataEntity
{
    public partial class Userdetail
    {
        public Userdetail()
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
