using CommonUtilities.CommonVariables;
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
                    return NotFound(Common.bookNotfoundWithFilters);
                }
                return Ok(books);
            }
            catch(Exception ex)
            {
                return Ok(Common.generalError);
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
                    return Json(NotFound(Common.bookNotfoundWithFilters));
                }
                return Json(books);
            }
            catch (Exception ex)
            {
                return Json(Common.generalError);
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
