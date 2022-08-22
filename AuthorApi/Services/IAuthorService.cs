using CommonUtilities.Model;
using CommonUtilities.ViewModels;
using System.Security.Claims;

namespace AuthorApi.Services
{
    public interface IAuthorService
    {
        BookDatabaseContext dbContext { get; set; }

        string CreateAccount(UserAccount userAccount);
        string ValidateAuthorCred(string userName, string userPassword, ClaimsIdentity identity);

        //AppAuthorizations appAuthorizations(ClaimsIdentity identity);
    }
}