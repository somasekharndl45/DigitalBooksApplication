using CommonUtilities.CommonVariables;
using CommonUtilities.Model;
using CommonUtilities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApiAuthentication.Services;

namespace WebApiAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private BookDatabaseContext dbContext { get; set; }

        private readonly ITokenService _tokenService;

        public AuthenticationController(IConfiguration configuration, BookDatabaseContext bookDatabaseContext, ITokenService tokenService)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            dbContext = bookDatabaseContext;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Authenticates the user after validating the user credentials
        /// </summary>
        /// <param name="userCredential">The userCredential object holds user name and password</param>
        /// <returns>action result</returns>
        [HttpPost]
        public JsonResult Authenticate(UserCredential userCredential)
        {
            try
            {
                IEnumerable<string> audience = new[]
                        {
                        _configuration["Jwt:ApiGatewayAudience"],
                        _configuration["Jwt:ReaderAudience"],
                        _configuration["Jwt:PaymentAudience"],
                        _configuration["Jwt:AuthorAudience"]
                        };
                bool isValidUser = ValidateUserCredentials(userCredential.UserName, userCredential.Password);
                if (isValidUser)
                {
                    var token = _tokenService.BuildToken(_configuration["Jwt:Key"],
                        _configuration["Jwt:Issuer"],
                        audience,
                        userCredential.UserName);
                    string userRole = dbContext.DigitalBooksUsers.Where(user => user.UserName == userCredential.UserName).Select(user => user.UserRole).FirstOrDefault();
                    return Json(new
                    {
                        Token = token,
                        Role = userRole
                    });
                }
                return Json(new
                {
                    Token = string.Empty,
                    Role = false
                });
            }
            catch(Exception ex)
            {
                return Json(Common.tokenError);
            }
        }

        /// <summary>
        /// Validates the user credentials by retrieving data from database with username and password
        /// </summary>
        /// <param name="userName">The unique userName</param>
        /// <param name="password">Password</param>
        /// <returns>True or False based on the credential</returns>
        private bool ValidateUserCredentials(string userName, string password)
        {
            bool isValidUser = !(dbContext.DigitalBooksUsers.Where(u => u.UserName == userName && u.UserPass == password).FirstOrDefault() is null) ? true : false;
            return isValidUser;
        }
    }
}

