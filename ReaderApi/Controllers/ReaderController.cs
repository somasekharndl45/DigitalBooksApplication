using CommonUtility.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReaderApi.Services;

namespace ReaderApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class ReaderController : Controller
    {
        private readonly IReaderBookService _bookService;

        public ReaderController(IReaderBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult SearchBooks([FromBody] BookAttributes bookAttributes)
        {
            try
            {
                var books = _bookService.GetBooks(bookAttributes);
                if (books.Count == 0)
                {
                    return Ok("Book not found for applied filters. Please change the filters and try again");
                }
                return Ok(books);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ReadPurchasedBook([FromBody] ReadBook readBook)
        {
            try
            {
                string result = _bookService.GetContentReadBook(readBook);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
