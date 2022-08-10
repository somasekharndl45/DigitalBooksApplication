using CommonUtility.DatabaseEntity;
using CommonUtility.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiAuthentication.Services;

namespace WebApiAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private DigitalBookDBContext DBContext { get; set; }

        private readonly ITokenService _tokenService;

        public AuthenticationController(IConfiguration configuration, DigitalBookDBContext DigitalBookDBContext, ITokenService tokenService)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            DBContext = DigitalBookDBContext;
            _tokenService = tokenService;
        }

        [HttpPost]
        public ActionResult<string> Authenticate(UserCredential userCredential)
        {
            try
            {
                if (ValidateUserCredentials(userCredential.UserName, userCredential.Password))
                {
                    var token = _tokenService.BuildToken(_configuration["Jwt:Key"],
                        _configuration["Jwt:Issuer"],
                        new[]
                        {
                        _configuration["Jwt:ApiGatewayAudience"],
                        _configuration["Jwt:ReaderAudience"],
                        _configuration["Jwt:PaymentAudience"],
                        _configuration["Jwt:AuthorAudience"]
                        },
                        userCredential.Password);
                    return Ok(new
                    {
                        Token = token,
                        IsAuthenticated = true
                    });
                }
                return Ok(new
                {
                    Token = string.Empty,
                    IsAuthenticated = false
                });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        private bool ValidateUserCredentials(string userName, string password)
        {
            bool isValidUser = !(DBContext.Authors.Where(u => u.AuthorName == userName && u.AuthorPassword == password).FirstOrDefault() is null) ? true : false;
            return isValidUser;
        }
    }
}

