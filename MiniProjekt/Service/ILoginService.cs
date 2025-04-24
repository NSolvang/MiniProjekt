using Core;

namespace MiniProjekt.Service;

public interface ILoginService
{
    Task<User> GetUserLoggedIn();
    
    Task<bool> Login(string UserName, string Password);
}