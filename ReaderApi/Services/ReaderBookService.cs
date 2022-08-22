using CommonUtilities.CommonVariables;
using CommonUtilities.Model;
using CommonUtilities.ViewModels;
using System.Linq;

namespace ReaderApi.Services
{
    public class ReaderBookService : IReaderBookService
    {
        public BookDatabaseContext dbContext { get; set; }

        public ReaderBookService(BookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

        /// <summary>
        /// Search available books
        /// </summary>
        /// <param name="bookAttributes">object has search fields to search book</param>
        /// <returns>list of available books</returns>
        public List<DisplayBookDetails> GetBooks(SearchBookFields searchBookFields)
        {
            List<DisplayBookDetails> bookList = new List<DisplayBookDetails>();
            if (!string.IsNullOrEmpty(searchBookFields.Author) || !string.IsNullOrEmpty(searchBookFields.Category) || !searchBookFields.Price.Equals(null))
            {
                var entity = dbContext.Books.Where(b => b.Active == true && (b.AuthorName == searchBookFields.Author
                    || b.Category == searchBookFields.Category || b.Price <= searchBookFields.Price));

                foreach (var item in entity)
                {
                    DisplayBookDetails bookDetails = new DisplayBookDetails();
                    bookDetails.AuthorName = item.AuthorName;
                    bookDetails.Publisher = item.Publisher;
                    bookDetails.PublishedDate = item.PublishedDate;
                    bookDetails.Category = item.Category;
                    bookDetails.Title = item.Title;
                    bookDetails.Price = item.Price;
                    bookDetails.Logo = item.Logo;
                    bookList.Add(bookDetails);
                }
                
                return bookList;
            }
            var entityAll = dbContext.Books.Where(b => b.Active == true);

            foreach (var item in entityAll)
            {
                DisplayBookDetails bookDetails = new DisplayBookDetails();
                bookDetails.AuthorName = item.AuthorName;
                bookDetails.Publisher = item.Publisher;
                bookDetails.PublishedDate = item.PublishedDate;
                bookDetails.Category = item.Category;
                bookDetails.Title = item.Title;
                bookDetails.Price = item.Price;
                bookDetails.Logo = item.Logo;
                bookList.Add(bookDetails);
            }
            return bookList;

            //if(bookAttributes.Category  == null && bookAttributes.Author == null && bookAttributes.Price == null)
            //{
            //    bookList = DBContext.Books.Select(s => s.Title).ToList();
            //    return bookList;
            //}
            //else if(bookAttributes.Category != null && bookAttributes.Author != null && bookAttributes.Price != null)
            //{
            //    bookList = DBContext.Books.Where(s => s.Category == bookAttributes.Category
            //    && s.AuthorName == bookAttributes.Author && s.Price == bookAttributes.Price)
            //    .Select(s => s.Title).ToList();
            //    return bookList;
            //}
            //bookList = DBContext.Books.Where(s => s.Category == bookAttributes.Category 
            //    && s.AuthorName == bookAttributes.Author && s.Price == bookAttributes.Price)
            //    .Select(s => s.Title).ToList();
            //return bookList;
        }

        public List<DisplayBookDetails> GetAllBook()
        {
            List<DisplayBookDetails> bookList = new List<DisplayBookDetails>();
            var entityAll = dbContext.Books.Where(b => b.Active == true);

            foreach (var item in entityAll)
            {
                DisplayBookDetails bookDetails = new DisplayBookDetails();
                bookDetails.AuthorName = item.AuthorName;
                bookDetails.Publisher = item.Publisher;
                bookDetails.PublishedDate = DateTime.UtcNow;
                bookDetails.Category = item.Category;
                bookDetails.Title = item.Title;
                bookDetails.Price = item.Price;
                bookDetails.Logo = item.Logo;
                bookList.Add(bookDetails);
            }
            return bookList;
        }

        /// <summary>
        /// GEt the book contetnt to read
        /// </summary>
        /// <param name="readBook">object has emailid and bookname</param>
        /// <returns>Book content</returns>
        public string GetContentReadBook(ReadBook readBook)
        {
            try
            {
                bool isPurchasedBook = false;
                string content = "";
                long bookID = dbContext.Books.Where(x => x.Title == readBook.BookName).Select(x => x.BookId).FirstOrDefault();
                isPurchasedBook = ((dbContext.Payments.Where(x => x.BuyerEmail == readBook.EmailId && x.BookId == bookID).Count()) > 0) ? true : false;
                if (isPurchasedBook)
                {
                    content = dbContext.Books.Where(x => x.BookId == bookID).Select(x => x.Content).FirstOrDefault();
                    return content;
                }
                else
                {
                    return Common.bookNotPurchased;
                }
            }
            catch (Exception ex)
            {
                return Common.generalError;
            }
        }
    }
}
