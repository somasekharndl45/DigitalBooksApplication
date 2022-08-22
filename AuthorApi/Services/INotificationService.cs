using CommonUtilities.Model;

namespace AuthorApi.Services
{
    public interface INotificationService
    {
        BookDatabaseContext dbContext { get; set; }

        //List<string>? sendNotification();
    }
}