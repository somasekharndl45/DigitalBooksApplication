using CommonUtility.DatabaseEntity;
using CommonUtility.Model;
using System.Linq;

namespace ReaderApi.Services
{
    public class ReaderBookService : IReaderBookService
    {
        public DigitalBookDBContext DBContext { get; set; }

        public ReaderBookService(DigitalBookDBContext DigitalBookDBContext)
        {
            DBContext = DigitalBookDBContext;
        }

        public List<BookDetails> GetBooks(BookAttributes bookAttributes)
        {
            List<BookDetails> bookList = new List<BookDetails>();
            if (!string.IsNullOrEmpty(bookAttributes.Author) || !string.IsNullOrEmpty(bookAttributes.Category) || !bookAttributes.Price.Equals(null))
            {
                var entity = DBContext.Books.Where(b => b.Active == true && (b.AuthorName == bookAttributes.Author
                    || b.Category == bookAttributes.Category || b.Price <= bookAttributes.Price));

                foreach (var item in entity)
                {
                    BookDetails bookDetails = new BookDetails();
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
            var entityAll = DBContext.Books.Where(b => b.Active == true);

            foreach (var item in entityAll)
            {
                BookDetails bookDetails = new BookDetails();
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

        public string GetContentReadBook(ReadBook readBook)
        {
            try
            {
                bool isPurchasedBook = false;
                string content = "";
                long bookID = DBContext.Books.Where(x => x.Title == readBook.BookName).Select(x => x.BookId).FirstOrDefault();
                isPurchasedBook = ((DBContext.Payments.Where(x => x.BuyerEmailId == readBook.EmailId && x.BookId == bookID).Count()) > 0) ? true : false;
                if (isPurchasedBook)
                {
                    content = DBContext.Books.Where(x => x.BookId == bookID).Select(x => x.Content).FirstOrDefault();
                    return content;
                }
                else
                {
                    return $"Purchase {readBook.BookName} to read";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
