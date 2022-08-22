using CommonUtilities.CommonVariables;
using CommonUtilities.Model;
using CommonUtilities.ViewModels;

namespace PaymentApi.Services
{
    public class PaymentService : IPaymentService
    {
        public BookDatabaseContext dbContext { get; set; }

        public PaymentService(BookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

        /// <summary>
        /// Make payment to buy
        /// </summary>
        /// <param name="buyer">Object has Buyer(reader) details</param>
        /// <returns></returns>
        public int BuyBook(Buyer buyer)
        {
                int bookId = dbContext.Books.Where(book => book.Title == buyer.BookName).Select(book => book.BookId).FirstOrDefault();                 
                Payment paymentEntity = new Payment();
                paymentEntity.BuyerName = buyer.BuyerName;
                paymentEntity.BuyerEmail = buyer.EmailId;
                paymentEntity.BookId = bookId;
                paymentEntity.PaymentDate = DateTime.Now;
                dbContext.Payments.Add(paymentEntity);
                dbContext.SaveChanges();
                int paymentId = dbContext.Payments.Where(p => p.PaymentDate == paymentEntity.PaymentDate).Select(p => p.PaymentId).FirstOrDefault();
                return paymentId;
        }

        /// <summary>
        /// Get Invoice
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns>Invoice</returns>
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

        /// <summary>
        /// Get payment hostory
        /// </summary>
        /// <param name="emailID">email id</param>
        /// <returns>payment history details</returns>
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

        /// <summary>
        /// Get refund
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns>Message on refund</returns>
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
