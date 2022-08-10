using CommonUtility.DatabaseEntity;
using CommonUtility.Model;

namespace ReaderApi.Services
{
    public interface IReaderBookService
    {
        DigitalBookDBContext DBContext { get; set; }

        List<BookDetails> GetBooks(BookAttributes bookAttributes);
        string GetContentReadBook(ReadBook readBook);
    }
}