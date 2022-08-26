using System.Security.Claims;
using CommonUtilities.Model;
using CommonUtilities.DataEntity;
using CommonUtilities.CommonVariables;

namespace AuthorApi.Services
{
    public class AuthorService : IAuthorService
    {
        public DigitalBookDatabaseContext dbContext { get; set; }

        public AuthorService(DigitalBookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

        
        public string CreateAccount(UserAccount userAccount)
        {
            try
            {
                var userItem = dbContext.Userdetails.Where(user => user.Email == userAccount.Email);
                if (userItem.Count() < 1)
                {
                    Userdetail bookUser = new Userdetail();
                    bookUser.UserName = userAccount.UserName;
                    bookUser.Email = userAccount.Email;
                    bookUser.UserPass = userAccount.UserPass;
                    bookUser.UserRole = userAccount.UserRole;
                    dbContext.Userdetails.Add(bookUser);
                    dbContext.SaveChanges();
                    return Common.userAccount;
                }
                else
                {
                    return Common.userAccountExists;
                }
            }
            catch (Exception ex)
            {
                return Common.userAccountCreationError;
            }
        }

        public string ValidateAuthorCred(string userName, string userPassword, ClaimsIdentity identity)
        {
            try
            {
                var authClaimRole = identity.FindFirst("Role").Value;
                var authRole = dbContext.Userdetails.Where(x => x.UserName == userName).Select(x => x.UserRole).FirstOrDefault();
                if (authRole != null)
                {
                    if (authClaimRole == authRole)
                    {
                        var password = dbContext.Userdetails.Where(x => x.UserName == userName).Select(x => x.UserPass).FirstOrDefault();
                        if (password == userPassword)
                        {
                            return Common.authorLoginSuccess;
                        }
                        else
                        {
                            return Common.credentialIncorrect;
                        }
                    }
                    else
                    {
                        return Common.unathuorized;
                    }
                }
                return Common.authorNotExists;                
            }
            catch (Exception ex)
            {
                return Common.authorLoginError;
            }
        }
    }
}
