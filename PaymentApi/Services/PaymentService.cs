
using CommonUtilities.Model;
using CommonUtilities.DataEntity;

namespace PaymentApi.Services
{
    public class PaymentService : IPaymentService
    {
        public DigitalBookDatabaseContext dbContext { get; set; }

        public PaymentService(DigitalBookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

       
        public int BuyBook(BuyerDetails buyerDetails)
        {
                int bookId = dbContext.Books.Where(book => book.Title == buyerDetails.BookName).Select(book => book.BookId).FirstOrDefault();                 
               CommonUtilities.DataEntity.Payment paymentEntity = new CommonUtilities.DataEntity.Payment();
                paymentEntity.BuyerName = buyerDetails.BuyerName;
                paymentEntity.BuyerEmail = buyerDetails.EmailId;
                paymentEntity.BookId = bookId;
                paymentEntity.PaymentDate = DateTime.Now;
                dbContext.Payments.Add(paymentEntity);
                dbContext.SaveChanges();
                int paymentId = dbContext.Payments.Where(p => p.PaymentDate == paymentEntity.PaymentDate).Select(p => p.PaymentId).FirstOrDefault();
                return paymentId;
        }

      
        public Invoice GetInvoice(int paymentId)
        {
            Payment paymentEntity = new Payment();
            paymentEntity = dbContext.Payments.Where(p => p.PaymentId == paymentId).FirstOrDefault();
            string bookName = dbContext.Books.Where(b => b.BookId == paymentEntity.BookId).Select(b => b.Title).FirstOrDefault();
            Invoice invoice = new Invoice();
            invoice.PaymentId = paymentId;
            invoice.PaymentDate = paymentEntity.PaymentDate;
            invoice.BuyerName = paymentEntity.BuyerName;
            invoice.BuyerEmailId = paymentEntity.BuyerEmail;
            invoice.BookName = bookName;
            return invoice;
        }

       
        public List<Invoice> GetPaymentHistory(string emailID)
        {
            List<Payment> paymentEntityList = new List<Payment>();
            paymentEntityList = dbContext.Payments.Where(x => x.BuyerEmail == emailID).ToList();

            List<Invoice> paymenthistoryList = new List<Invoice>();

            foreach (var item in paymentEntityList)
            {
                Invoice invoice = new Invoice();
                invoice.PaymentId = item.PaymentId;
                invoice.PaymentDate = item.PaymentDate;
                invoice.BuyerName = item.BuyerName;
                invoice.BuyerEmailId = emailID;
                string bookName = dbContext.Books.Where(b => b.BookId == item.BookId).Select(b => b.Title).FirstOrDefault();
                invoice.BookName = bookName;
                paymenthistoryList.Add(invoice);
            }
            
            return paymenthistoryList;
        }

       
        public string GetRefund(int paymentId)
        {
            DateTime paymentDate = dbContext.Payments.Where(x => x.PaymentId == paymentId).Select(x => x.PaymentDate).FirstOrDefault();
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
