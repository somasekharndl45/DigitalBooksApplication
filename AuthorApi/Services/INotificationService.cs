using CommonUtilities.Model;
using CommonUtilities.DataEntity;

namespace AuthorApi.Services
{
    public interface INotificationService
    {
        DigitalBookDatabaseContext dbContext { get; set; }

        //List<string>? sendNotification();
    }
}