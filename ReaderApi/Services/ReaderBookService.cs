using CommonUtilities.CommonVariables;
using CommonUtilities.Model;
using CommonUtilities.DataEntity;

namespace ReaderApi.Services
{
    public class ReaderBookService : IReaderBookService
    {
        public DigitalBookDatabaseContext dbContext { get; set; }

        public ReaderBookService(DigitalBookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }
        public List<BookInformation> GetBooks(BookProperties searchBookFields)
        {
            List<BookInformation> bookList = new List<BookInformation>();
            if (!string.IsNullOrEmpty(searchBookFields.Author) || !string.IsNullOrEmpty(searchBookFields.Category) || !searchBookFields.Price.Equals(null))
            {
                var entity = dbContext.Books.Where(b => b.Active == true && (b.AuthorName == searchBookFields.Author
                    || b.Category == searchBookFields.Category || b.Price <= searchBookFields.Price));

                foreach (var item in entity)
                {
                    BookInformation bookDetails = new BookInformation();
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
                BookInformation bookDetails = new BookInformation();
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

        public List<BookInformation> GetAllBook()
        {
            List<BookInformation> bookList = new List<BookInformation>();
            var entityAll = dbContext.Books.Where(b => b.Active == true);

            foreach (var item in entityAll)
            {
                BookInformation bookDetails = new BookInformation();
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

      
        public string GetContentReadBook(BookRead readBook)
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
