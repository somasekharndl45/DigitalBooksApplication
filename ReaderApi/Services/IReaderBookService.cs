using CommonUtilities.Model;
using CommonUtilities.DataEntity;
namespace ReaderApi.Services
{
    public interface IReaderBookService
    {
        DigitalBookDatabaseContext dbContext { get; set; }

        List<BookInformation> GetAllBook();
        List<BookInformation> GetBooks(BookProperties searchBookFields);
        string GetContentReadBook(BookRead readBook);
    }
}