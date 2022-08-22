using CommonUtilities.CommonVariables;
using CommonUtilities.ViewModels;
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

        /// <summary>
        /// Search available books
        /// </summary>
        /// <param name="searchBookFields">object has search fields to search book</param>
        /// <returns>message on search book</returns>
        [HttpGet]
        public ActionResult SearchBooks([FromBody] SearchBookFields searchBookFields)
        {
            try
            {
                var books = _bookService.GetBooks(searchBookFields);
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
        public ActionResult ReadPurchasedBook([FromBody] ReadBook readBook)
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
