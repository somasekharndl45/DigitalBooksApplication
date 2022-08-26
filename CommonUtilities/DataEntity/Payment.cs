using System;
using System.Collections.Generic;

namespace CommonUtilities.DataEntity
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public string BuyerName { get; set; } = null!;
        public int BookId { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
