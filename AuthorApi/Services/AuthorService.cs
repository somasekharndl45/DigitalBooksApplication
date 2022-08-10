using CommonUtility.DatabaseEntity;

namespace AuthorApi.Services
{
    public class AuthorService : IAuthorService
    {
        public DigitalBookDBContext DBContext { get; set; }

        public AuthorService(DigitalBookDBContext DigitalBookDBContext)
        {
            DBContext = DigitalBookDBContext;
        }

        public string AddAccount(string userName, string password)
        {
            try
            {
                var authorNameList = DBContext.Authors.Select(x => x.AuthorName).ToList();
                if (!(authorNameList.Contains(userName)))
                {
                    Author author = new Author();
                    author.AuthorName = userName;
                    author.AuthorPassword = password;
                    DBContext.Authors.Add(author);
                    DBContext.SaveChanges();
                    return "Author account created Successfully";
                }
                else
                {
                    return "Author Name already exists";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ValidateAuthorCred(string userName, string userPassword)
        {
            try
            {
                var authorNameList = DBContext.Authors.Select(x => x.AuthorName).ToList();
                if (authorNameList.Contains(userName))
                {
                    var password = DBContext.Authors.Where(x => x.AuthorName == userName).Select(x => x.AuthorPassword).FirstOrDefault();
                    if (password == userPassword)
                    {
                        return "User logged in successfully";
                    }
                    else
                    {
                        return "UserName or Password is incorrect. Please try again";
                    }
                }
                else
                {
                    return "Account doesn't exist. Please create account";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
