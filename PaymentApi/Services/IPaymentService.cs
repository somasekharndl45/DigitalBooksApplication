using CommonUtilities.Model;
using CommonUtilities.DataEntity;

namespace PaymentApi.Services
{
    public interface IPaymentService
    {
        DigitalBookDatabaseContext dbContext { get; set; }

        int BuyBook(BuyerDetails buyer);
        Invoice GetInvoice(int paymentId);
        List<Invoice> GetPaymentHistory(string emailID);
        string GetRefund(int paymentId);
    }
}