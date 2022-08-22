using System.Security.Claims;
using CommonUtilities.ViewModels;
using CommonUtilities.Model;
using CommonUtilities.CommonVariables;

namespace AuthorApi.Services
{
    public class AuthorService : IAuthorService
    {
        public BookDatabaseContext dbContext { get; set; }

        public AuthorService(BookDatabaseContext bookDatabaseContext)
        {
            dbContext = bookDatabaseContext;
        }

        /// <summary>
        /// Used to create user acccount
        /// </summary>
        /// <param name="userAccount"> holds user details</param>
        /// <returns>message on user account creation</returns>
        public string CreateAccount(UserAccount userAccount)
        {
            try
            {
                var userItem = dbContext.DigitalBooksUsers.Where(user => user.Email == userAccount.Email);
                if (userItem.Count() < 1)
                {
                    DigitalBooksUser bookUser = new DigitalBooksUser();
                    bookUser.UserName = userAccount.UserName;
                    bookUser.Email = userAccount.Email;
                    bookUser.UserPass = userAccount.UserPass;
                    bookUser.UserRole = userAccount.UserRole;
                    dbContext.DigitalBooksUsers.Add(bookUser);
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

        /// <summary>
        /// Validates the user credentials for login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="identity"></param>
        /// <returns>message on login</returns>
        public string ValidateAuthorCred(string userName, string userPassword, ClaimsIdentity identity)
        {
            try
            {
                var authClaimRole = identity.FindFirst("Role").Value;
                var authRole = dbContext.DigitalBooksUsers.Where(x => x.UserName == userName).Select(x => x.UserRole).FirstOrDefault();
                if (authRole != null)
                {
                    if (authClaimRole == authRole)
                    {
                        var password = dbContext.DigitalBooksUsers.Where(x => x.UserName == userName).Select(x => x.UserPass).FirstOrDefault();
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
