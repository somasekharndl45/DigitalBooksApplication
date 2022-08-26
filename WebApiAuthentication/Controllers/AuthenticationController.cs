using CommonUtilities.CommonVariables;
using CommonUtilities.Model;
using Microsoft.AspNetCore.Mvc;
using WebApiAuthentication.Services;
using CommonUtilities.DataEntity;

namespace WebApiAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private DigitalBookDatabaseContext dbContext { get; set; }

        private readonly ITokenService _tokenService;

        public AuthenticationController(IConfiguration configuration, DigitalBookDatabaseContext bookDatabaseContext, ITokenService tokenService)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            dbContext = bookDatabaseContext;
            _tokenService = tokenService;
        }


        [HttpPost]
        public JsonResult Authentication(UserCredentials userCredential)
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
                    string userRole = dbContext.Userdetails.Where(user => user.UserName == userCredential.UserName).Select(user => user.UserRole).FirstOrDefault();
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

       
        private bool ValidateUserCredentials(string userName, string password)
        {
            bool isValidUser = !(dbContext.Userdetails.Where(u => u.UserName == userName && u.UserPass == password).FirstOrDefault() is null) ? true : false;
            return isValidUser;
        }
    }
}

