using CommonUtilities.Model;

namespace AuthorApi.Services
{
    public class NotificationService : INotificationService
    {
        public BookDatabaseContext dbContext { get; set; }

        public NotificationService(BookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

        /// <summary>
        /// Send Notification to reader if the book is blocked
        /// </summary>
        /// <returns></returns>
        //public List<string>? sendNotification()
        //{
        //    var inactiveIds = dbContext.Books.Where(b => b.Active == false).Select(s => s.BookId).ToList();

        //    if (inactiveIds.Count > 0)
        //    {
        //        var inactivebooks = dbContext.Books.Where(b => b.Active == false).Select(s => s.Title).ToList();
        //        return inactivebooks;
        //        //var paymentDetails = DBContext.Payments.ToList();
        //        //List<string> usersList = new List<string>();

        //        //foreach (var item in paymentDetails)
        //        //{
        //        //    if (inactiveIds.Contains(Convert.ToInt64(item.BookId)))
        //        //    {

        //        //    }
        //        //}
        //    }
        //    return null;
        //}
    }
}
