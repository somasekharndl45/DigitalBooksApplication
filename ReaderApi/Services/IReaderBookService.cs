using CommonUtilities.Model;
using CommonUtilities.ViewModels;

namespace ReaderApi.Services
{
    public interface IReaderBookService
    {
        BookDatabaseContext dbContext { get; set; }

        List<DisplayBookDetails> GetAllBook();
        List<DisplayBookDetails> GetBooks(SearchBookFields searchBookFields);
        string GetContentReadBook(ReadBook readBook);
    }
}