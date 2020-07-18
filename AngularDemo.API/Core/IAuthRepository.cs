using System.Threading.Tasks;
using AngularDemo.API.Models;

namespace AngularDemo.API.Core
{
    public interface IAuthRepository
    {
         Task<WebUser> Register(WebUser User,string Password);
         Task<WebUser> Login(string EmailAddress,string Password);
         Task<bool> UserExist(string EmailAddress);

    }
}