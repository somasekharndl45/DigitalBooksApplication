using AuthorApi.Services;
using CommonUtilities.CommonVariables;
using CommonUtilities.ViewModels;
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

        /// <summary>
        /// Used to create user account for both reader and author
        /// </summary>
        /// <param name="userAccount">object holds user related data</param>
        /// <returns>Message upon successful creation on user account</returns>
        [HttpPost]
        public JsonResult CreateUserAccount([FromBody] UserAccount userAccount)
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

        /// <summary>
        /// Author login authorization
        /// </summary>
        /// <param name="userCredential"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AuthorLogin([FromBody] UserCredential userCredential)
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

        /// <summary>
        /// Post Method to create new book
        /// </summary>
        /// <param name="addBook">Object has book details</param>
        /// <returns>message on book creation</returns>
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

        /// <summary>
        /// To edit the book
        /// </summary>
        /// <param name="editBook">object with edit details of book</param>
        /// <returns>Message on edit book</returns>
        [HttpPut]
        public ActionResult EditBook([FromBody] EditBook editBook)
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

        /// <summary>
        /// Block or Unblock book
        /// </summary>
        /// <param name="blockBook">Object has data to unblock or block book</param>
        /// <returns>message on block/ unblock book</returns>
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

        /// <summary>
        /// Send Notification to reader if the book is blocked
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult NotifyReader()
        //{
        //    try
        //    {
        //        var result = _notificationService.sendNotification();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}

    }
}
