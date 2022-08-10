using AuthorApi.Services;
using CommonUtility.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        private readonly IBookService _bookService;

        private readonly INotificationService _notificationService;

        public AuthorController(IAuthorService authorService, IBookService bookService, INotificationService notificationService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _notificationService = notificationService;
        }

        [HttpPost]
        public ActionResult CreateAccount([FromBody] UserCredential userCredential)
        {
            try
            {
                string result = _authorService.AddAccount(userCredential.UserName, userCredential.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public ActionResult Login([FromBody] UserCredential userCredential)
        {
            try
            {
                string result = _authorService.ValidateAuthorCred(userCredential.UserName, userCredential.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public ActionResult CreateBook([FromBody] BookTable book)
        {
            try
            {
                string result = _bookService.CreateBook(book);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPut]
        public ActionResult EditBook([FromBody] BookTable book)
        {
            try
            {
                string result = _bookService.UpdateBook(book);
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
                string  result = _bookService.BlockorUnblockActiveBook(blockBook);                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public ActionResult NotifyReader()
        {
            try
            {
                var result = _notificationService.sendNotification();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

    }
}
