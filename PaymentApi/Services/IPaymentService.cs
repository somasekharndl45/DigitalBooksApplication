using CommonUtility.DatabaseEntity;
using CommonUtility.Model;

namespace PaymentApi.Services
{
    public interface IPaymentService
    {
        DigitalBookDBContext DBContext { get; set; }

        string BuyBook(Buyer buyer);
        Invoice GetInvoice(long paymentId);
        List<Invoice> GetPaymentHistory(string emailID);
        string GetRefund(long paymentId);
    }
}