using CommonUtility.DatabaseEntity;
using CommonUtility.Model;

namespace PaymentApi.Services
{
    public class PaymentService : IPaymentService
    {
        public DigitalBookDBContext DBContext { get; set; }

        public PaymentService(DigitalBookDBContext DigitalBookDBContext)
        {
            DBContext = DigitalBookDBContext;
        }

        public string BuyBook(Buyer buyer)
        {
            try
            {
                long bookId = DBContext.Books.Where(book => book.Title == buyer.BookName).Select(book => book.BookId).FirstOrDefault();                 
                Payment paymentEntity = new Payment();
                paymentEntity.BuyerName = buyer.BuyerName;
                paymentEntity.BuyerEmailId = buyer.EmailId;
                paymentEntity.BookId = bookId;
                paymentEntity.PaymentDate = DateTime.Now;
                DBContext.Payments.Add(paymentEntity);
                DBContext.SaveChanges();
                long paymentId = DBContext.Payments.Where(p => p.PaymentDate == paymentEntity.PaymentDate).Select(p => p.PaymentId).FirstOrDefault();
                return $"Your payment is successful and you payment id is {paymentId}";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public Invoice GetInvoice(long paymentId)
        {
            Payment paymentEntity = new Payment();
            paymentEntity = DBContext.Payments.Where(p => p.PaymentId == paymentId).FirstOrDefault();
            string bookName = DBContext.Books.Where(b => b.BookId == paymentEntity.BookId).Select(b => b.Title).FirstOrDefault();
            Invoice invoice = new Invoice();
            invoice.PaymentId = paymentId;
            invoice.PaymentDate = paymentEntity.PaymentDate;
            invoice.BuyerName = paymentEntity.BuyerName;
            invoice.BuyerEmailId = paymentEntity.BuyerEmailId;
            invoice.BookName = bookName;
            return invoice;
        }

        public List<Invoice> GetPaymentHistory(string emailID)
        {
            List<Payment> paymentEntityList = new List<Payment>();
            paymentEntityList = DBContext.Payments.Where(x => x.BuyerEmailId == emailID).ToList();

            List<Invoice> paymenthistoryList = new List<Invoice>();

            foreach (var item in paymentEntityList)
            {
                Invoice invoice = new Invoice();
                invoice.PaymentId = item.PaymentId;
                invoice.PaymentDate = item.PaymentDate;
                invoice.BuyerName = item.BuyerName;
                invoice.BuyerEmailId = emailID;
                string bookName = DBContext.Books.Where(b => b.BookId == item.BookId).Select(b => b.Title).FirstOrDefault();
                invoice.BookName = bookName;
                paymenthistoryList.Add(invoice);
            }
            
            return paymenthistoryList;
        }

        public string GetRefund(long paymentId)
        {
            DateTime paymentDate = DBContext.Payments.Where(x => x.PaymentId == paymentId).Select(x => x.PaymentDate).FirstOrDefault();
            bool isLessThan24Hours = Math.Abs(paymentDate.Subtract(DateTime.Now).TotalHours) <= 24;
            if (isLessThan24Hours)
            {
                return "Refund will be provided in 24 hours";
            }
            else
            {
                return "Refund can be provided only within 24 hours of payment";
            }
        }
    }
}
