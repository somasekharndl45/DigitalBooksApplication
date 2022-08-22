using CommonUtilities.Model;
using CommonUtilities.ViewModels;

namespace AuthorApi.Services
{
    public interface IBookService
    {
        BookDatabaseContext dbContext { get; set; }

        string CreateBook(AddBook addBook);
        string BlockorUnblockActiveBook(BlockBook blockBook);
        string UpdateBook(EditBook editBook);
    }
}