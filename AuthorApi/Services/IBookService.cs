using CommonUtilities.Model;
using CommonUtilities.DataEntity;

namespace AuthorApi.Services
{
    public interface IBookService
    {
        DigitalBookDatabaseContext dbContext { get; set; }

        string CreateBook(AddBook addBook);
        string BlockorUnblockActiveBook(BlockBook blockBook);
        string UpdateBook(BookTable editBook);
    }
}