using Blazored.LocalStorage;
using Core;
using MiniProjekt.Service;

namespace MiniProjekt.Service;

public class LoginServiceClientSlide : ILoginService
{
    private readonly ILocalStorageService localStorage;

    public LoginServiceClientSlide(ILocalStorageService ls)
    {
        localStorage = ls;
    }

    public async Task<User?> GetUserLoggedIn()
    {
        return await localStorage.GetItemAsync<User>("user");
    }

    public async Task<bool> Login(string userName, string password)
    {
        // This is just a placeholder - in a real app you'd call an API
        if (userName.Equals("demo") && password.Equals("demo123"))
        {
            User user = new User 
            { 
                Username = userName, 
                Password = "verified" 
            };
            await localStorage.SetItemAsync("user", user);
            return true;
        }
        return false;
    }

    public async Task AddUser(User user)
    {
        await localStorage.SetItemAsync("user", user);
    }
    
    public async Task LogOut()
    {
        await localStorage.RemoveItemAsync("user");
    }
}