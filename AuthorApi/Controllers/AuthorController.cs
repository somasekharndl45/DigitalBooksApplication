using AuthorApi.Services;
using CommonUtilities.CommonVariables;
using CommonUtilities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthorApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        private readonly IBookService _bookService;

        private readonly INotificationService _notificationService;

        private IEnumerable<Claim> _claims;

        public AuthorController(IAuthorService authorService, IBookService bookService, INotificationService notificationService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _notificationService = notificationService;
            
        }

        [HttpPost]
        public JsonResult CreateAccount([FromBody] UserAccount userAccount)
        {
            try
            {
                string result = _authorService.CreateAccount(userAccount);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpGet]
        public ActionResult Login([FromBody] UserCredentials userCredential)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    string result = _authorService.ValidateAuthorCred(userCredential.UserName, userCredential.Password, identity);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public JsonResult CreateBook([FromBody] AddBook addBook)
        {
            try
            {
                string result = _bookService.CreateBook(addBook);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(Common.bookAddedMsg);
            }
        }
        [HttpPut]
        public ActionResult EditBook([FromBody] BookTable editBook)
        {
            try
            {
                string result = _bookService.UpdateBook(editBook);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

       
        [HttpPut]
        public ActionResult BlockorUnlockBook([FromBody] BlockBook blockBook)
        {
            try
            {
                string result = _bookService.BlockorUnblockActiveBook(blockBook);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
