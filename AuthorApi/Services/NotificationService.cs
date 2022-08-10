using CommonUtility.DatabaseEntity;
using System.Linq;

namespace AuthorApi.Services
{
    public class NotificationService : INotificationService
    {
        public DigitalBookDBContext DBContext { get; set; }

        public NotificationService(DigitalBookDBContext digitalBookDBContext)
        {
            DBContext = digitalBookDBContext;
        }

        public List<string>? sendNotification()
        {
            var inactiveIds = DBContext.Books.Where(b => b.Active == false).Select(s => s.BookId).ToList();

            if (inactiveIds.Count > 0)
            {
                var inactivebooks = DBContext.Books.Where(b => b.Active == false).Select(s => s.Title).ToList();
                return inactivebooks;
                //var paymentDetails = DBContext.Payments.ToList();
                //List<string> usersList = new List<string>();

                //foreach (var item in paymentDetails)
                //{
                //    if (inactiveIds.Contains(Convert.ToInt64(item.BookId)))
                //    {

                //    }
                //}
            }
            return null;
        }
    }
}
