
using CommonUtilities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReaderApi.Services;

namespace ReaderApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReaderController : Controller
    {
        private readonly IReaderBookService _bookService;

        public ReaderController(IReaderBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public ActionResult SearchBooks([FromBody] BookProperties bookProperties)
        {
            try
            {
                var books = _bookService.GetBooks(bookProperties);
                if (books.Count == 0)
                {
                    return NotFound("No book found change the filters and try ");
                }
                return Ok(books);
            }
            catch(Exception)
            {
                return Ok("Some error occurred");
            }
        }

        [HttpGet]
        public JsonResult DisplayBooks()
        {
            try
            {
                var books = _bookService.GetAllBook();
                if (books.Count == 0)
                {
                    return Json(NotFound("No book found change the filters and try "));
                }
                return Json(books);
            }
            catch (Exception )
            {
                return Json("Some error occurred");
            }
        }

        [HttpGet]
        public ActionResult ReadPurchasedBook([FromBody] BookRead readBook)
        {
            try
            {
                string result = _bookService.GetContentReadBook(readBook);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
