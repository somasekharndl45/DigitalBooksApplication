using System;
using System.Collections.Generic;

namespace CommonUtility.DatabaseEntity
{
    public partial class Payment
    {
        public long PaymentId { get; set; }
        public string BuyerEmailId { get; set; } = null!;
        public string BuyerName { get; set; } = null!;
        public long? BookId { get; set; }
        public DateTime PaymentDate { get; set; }
        public virtual Book? Book { get; set; }
    }
}
