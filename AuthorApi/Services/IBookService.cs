using CommonUtility.DatabaseEntity;
using CommonUtility.Model;

namespace AuthorApi.Services
{
    public interface IBookService
    {
        DigitalBookDBContext DBContext { get; set; }

        string CreateBook(BookTable bookTable);
        string BlockorUnblockActiveBook(BlockBook blockBook);
        string UpdateBook(BookTable bookTable);
    }
}