using CommonUtilities.Model;
using CommonUtilities.ViewModels;

namespace PaymentApi.Services
{
    public interface IPaymentService
    {
        BookDatabaseContext dbContext { get; set; }

        int BuyBook(Buyer buyer);
        Invoice GetInvoice(int paymentId);
        List<Invoice> GetPaymentHistory(string emailID);
        string GetRefund(int paymentId);
    }
}