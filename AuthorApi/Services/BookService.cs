using CommonUtilities.Model;
using CommonUtilities.DataEntity;

namespace AuthorApi.Services
{
    public class BookService : IBookService
    {
        public DigitalBookDatabaseContext dbContext { get; set; }

        public BookService(DigitalBookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

      
        public string CreateBook(AddBook addBook)
        {
            try
            {
                if (!(dbContext.Userdetails.Where(x => x.UserName == addBook.AuthorName).First() is null))
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
                    return "Book added successfully";
                }
                else
                {
                    return "Author doesn't exists";
                }
            }
            catch (Exception)
            {
                return "Some error occurred";
            }
        }

     
        public string UpdateBook(BookTable editBook)
        {
            try
            {
                var bookToUpdate = dbContext.Books.FirstOrDefault(x => x.BookId == editBook.BookId);
                if (bookToUpdate != null)
                {
                    bookToUpdate.Price = editBook.Price;
                    bookToUpdate.Category = editBook.Category;
                    bookToUpdate.Title = editBook.Title;
                    bookToUpdate.ModifiedDate = DateTime.Now;
                    bookToUpdate.Publisher = editBook.Publisher;
                    if (!string.IsNullOrEmpty(editBook.Content))
                    {
                        bookToUpdate.Content = editBook.Content;
                    }
                    dbContext.SaveChanges();
                    return "Edited book successfully";
                }
                else
                {
                    return "Book Not found";
                }
            }
            catch (Exception)
            {
                return "Error occurred in edit book";
            }
        }

       
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
                    return "Blocked book successfully";
                }
                else
                {
                    return "Blocked book successfully";
                }
            }
            return "Book Not found";
        }
    }
}
