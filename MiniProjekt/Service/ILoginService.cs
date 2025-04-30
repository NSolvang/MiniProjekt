using Core;

namespace MiniProjekt.Service;

public interface ILoginService
{
    Task<bool> Login(string userName, string password);
    Task<User?> GetUserLoggedIn();
    Task AddUser(User user);

    Task LogOut();

}