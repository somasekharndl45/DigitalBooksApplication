using CommonUtility.DatabaseEntity;

namespace AuthorApi.Services
{
    public interface INotificationService
    {
        DigitalBookDBContext DBContext { get; set; }

        List<string>? sendNotification();
    }
}