using CommonUtilities.Model;
using CommonUtilities.ViewModels;
using CommonUtilities.CommonVariables;
using System.Linq;

namespace AuthorApi.Services
{
    public class BookService : IBookService
    {
        public BookDatabaseContext dbContext { get; set; }

        public BookService(BookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

        /// <summary>
        /// Method to create new book
        /// </summary>
        /// <param name="addBook">object has book details</param>
        /// <returns>message on book creation</returns>
        public string CreateBook(AddBook addBook)
        {
            try
            {
                if (!(dbContext.DigitalBooksUsers.Where(x => x.UserName == addBook.AuthorName).First() is null))
                {
                    Book bookEntity = new Book();
                    bookEntity.Logo = addBook.Logo;
                    bookEntity.Title = addBook.Title;
                    bookEntity.Category = addBook.Category;
                    bookEntity.Price = addBook.Price;
                    bookEntity.AuthorName = addBook.AuthorName;
                    bookEntity.Publisher = addBook.Publisher;
                    bookEntity.PublishedDate = DateTime.UtcNow;
                    bookEntity.CreatedDate = DateTime.Now;
                    bookEntity.ModifiedDate = DateTime.Now;
                    bookEntity.Active = addBook.Active;
                    bookEntity.Content = addBook.Content;


                    dbContext.Books.Add(bookEntity);
                    dbContext.SaveChanges();
                    return Common.bookAddedMsg;
                }
                else
                {
                    return Common.bookNotAddedMsg;
                }
            }
            catch (Exception ex)
            {
                return Common.generalError;
            }
        }

        /// <summary>
        /// Modify book attributes
        /// </summary>
        /// <param name="editBook">has value to be modified</param>
        /// <returns>Message on modify book</returns>
        public string UpdateBook(EditBook editBook)
        {
            try
            {
                var bookToUpdate = dbContext.Books.FirstOrDefault(x => x.BookId == editBook.BookId);
                if (bookToUpdate != null)
                {
                    bookToUpdate.Title = editBook.Title;
                    bookToUpdate.ModifiedDate = DateTime.Now;
                    bookToUpdate.Publisher = editBook.Publisher;
                    if (!string.IsNullOrEmpty(editBook.Content))
                    {
                        bookToUpdate.Content = editBook.Content;
                    }
                    dbContext.SaveChanges();
                    return Common.editbookMsg;
                }
                else
                {
                    return Common.bookNotFound;
                }
            }
            catch (Exception ex)
            {
                return Common.editbookErrorMsg;
            }
        }

        /// <summary>
        /// Block book
        /// </summary>
        /// <param name="blockBook">Object has params to block book</param>
        /// <returns>message on block / unblock</returns>
        public string BlockorUnblockActiveBook(BlockBook blockBook)
        {
            var book = dbContext.Books.Where(x => x.Title == blockBook.BookName && x.AuthorName == blockBook.AuthorName).FirstOrDefault();
            if (book != null)
            {
                var entity = dbContext.Books.Where(b => b.Title == blockBook.BookName).FirstOrDefault();
                if (blockBook.Block)
                {
                    entity.Active = false;
                }
                else
                {
                    entity.Active = true;
                }
                dbContext.Books.Update(entity);
                dbContext.SaveChanges();
                if (blockBook.Block)
                {
                    return Common.blockbookMsg;
                }
                else
                {
                    return Common.unblockbookMsg;
                }
            }
            return Common.bookNotFound;
        }
    }
}
