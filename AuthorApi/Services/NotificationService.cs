using CommonUtilities.Model;
using CommonUtilities.DataEntity;

namespace AuthorApi.Services
{
    public class NotificationService : INotificationService
    {
        public DigitalBookDatabaseContext dbContext { get; set; }

        public NotificationService(DigitalBookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

    }
}
