using System.Security.Claims;

namespace Author.Services
{
    public class Appclaims
    {
        public string UserType { get; set; }
        public string AuthorName { get; set; }
        public string UserName { get; set; }

        public Appclaims(ClaimsIdentity claimIdentity)
        {
        //    this.UserType = claimIdentity.FindFirst("UserType").Value;
        //    this.UserName = claimIdentity.FindFirst("UserName").Value;
            this.AuthorName = claimIdentity.FindFirst("AuthorName").Value;
        }
    }
}
