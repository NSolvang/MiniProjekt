using Blazored.LocalStorage;
using Core;
using MiniProjekt.Service;

namespace SwapClothes.Service;

public class LoginServiceClientSlide: ILoginService
{
    private ILocalStorageService localStorage { get; set; }

    public LoginServiceClientSlide(ILocalStorageService ls)
    {
        localStorage = ls;
    }

    public async Task<User?> GetUserLoggedIn()
    {
        var res = await localStorage.GetItemAsync<User>("user");
        return res;
    }

    public async Task<bool> Login(string UserName, string Password)
    {
        if (UserName.Equals("peter") && Password.Equals("1234"))
        {
            User user = new User { Username = UserName, Password = "verfied"};
            await localStorage.SetItemAsync("user", user);
        }

        return true;
    }
    
}