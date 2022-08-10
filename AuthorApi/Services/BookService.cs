using CommonUtility.DatabaseEntity;
using CommonUtility.Model;
using System.Linq;

namespace AuthorApi.Services
{
    public class BookService : IBookService
    {
        public DigitalBookDBContext DBContext { get; set; }

        public BookService(DigitalBookDBContext digitalBookDBContext)
        {
            DBContext = digitalBookDBContext;
        }

        public string CreateBook(BookTable bookTable)
        {
            try
            {
                if(!(DBContext.Authors.Where(x => x.AuthorName == bookTable.AuthorName).First() is null))
                {
                    Book bookEntity = new Book();
                    bookEntity.Logo = bookTable.Logo;
                    bookEntity.Title = bookTable.Title;
                    bookEntity.Category = bookTable.Category;
                    bookEntity.Price = bookTable.Price;
                    bookEntity.AuthorName = bookTable.AuthorName;
                    bookEntity.Publisher = bookTable.Publisher;
                    bookEntity.PublishedDate = bookTable.PublishedDate;
                    bookEntity.CreatedDate = DateTime.Now;
                    bookEntity.ModifiedDate = DateTime.Now;
                    bookEntity.Active = bookTable.Active;
                    bookEntity.Content = bookTable.Content;


                    DBContext.Books.Add(bookEntity);
                    DBContext.SaveChanges();
                    return "Book added successfully";
                }
                else 
                {
                    return "Author name doesn't exist";
                }                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateBook(BookTable bookTable)
        {
            try
            {
                var bookToUpdate = DBContext.Books.FirstOrDefault(x => x.BookId == bookTable.BookId);
                if (bookToUpdate != null)
                {
                    bookToUpdate.Title = bookTable.Title;
                    bookToUpdate.ModifiedDate = DateTime.Now;
                    bookToUpdate.Publisher = bookTable.Publisher;
                    if (!string.IsNullOrEmpty(bookTable.Content))
                    {
                        bookToUpdate.Content = bookTable.Content;
                    }
                    DBContext.SaveChanges();
                    return $"Updated book id: {bookTable.BookId} Successfully";
                }
                else
                {
                    return $"Book Id : {bookTable.BookId} doesn't exist ";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string BlockorUnblockActiveBook(BlockBook blockBook)
        {
            var book = DBContext.Books.Where(x => x.Title == blockBook.BookName && x.AuthorName == blockBook.AuthorName).FirstOrDefault();
            if(book != null)
            {
                var entity = DBContext.Books.Where(b => b.Title == blockBook.BookName).FirstOrDefault();
                if(blockBook.Block)
                {
                    entity.Active = false;
                }
                else
                {
                    entity.Active = true;
                }
                DBContext.Books.Update(entity);
                DBContext.SaveChanges();
                if(blockBook.Block)
                {
                    return $"Book {blockBook.BookName} blocked successfully";
                }
                else
                {
                    return $"Book {blockBook.BookName} unblocked successfully";
                }
            }
            return "Book doesn't exist. Try again with other book names";
        }
    }
}
