using CommonUtilities.Model;
using System.Security.Claims;
using CommonUtilities.DataEntity;

namespace AuthorApi.Services
{
    public interface IAuthorService
    {
        DigitalBookDatabaseContext dbContext { get; set; }

        string CreateAccount(UserAccount userAccount);
        string ValidateAuthorCred(string userName, string userPassword, ClaimsIdentity identity);
        IEnumerable<Book> GetAllBooks(string authorName);

        //AppAuthorizations appAuthorizations(ClaimsIdentity identity);
    }
}