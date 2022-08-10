using CommonUtility.DatabaseEntity;

namespace AuthorApi.Services
{
    public interface IAuthorService
    {
        DigitalBookDBContext DBContext { get; set; }

        string AddAccount(string userName, string password);
        string ValidateAuthorCred(string userName, string userPassword);
    }
}