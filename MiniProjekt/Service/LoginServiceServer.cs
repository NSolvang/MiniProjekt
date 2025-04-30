using Core;
using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace MiniProjekt.Service;

public class LoginServiceServer : ILoginService
{
    private readonly string serverUrl = "http://localhost:5063/";
    private readonly HttpClient client;
    private readonly ILocalStorageService localStorage;

    public LoginServiceServer(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        client = httpClient;
        localStorage = localStorageService;
    }

    public async Task<User?> GetUserLoggedIn() =>
        await localStorage.GetItemAsync<User>("user");

    public async Task<bool> Login(string username, string password)
    {
        try
        {
            var response = await client.PostAsJsonAsync(
                $"{serverUrl}api/User/login",
                new { Username = username, Password = password }
            );

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                if (user != null)
                {
                    await localStorage.SetItemAsync("user", user);
                    return true;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task AddUser(User user)
    {
        
        await client.PostAsJsonAsync($"{serverUrl}api/User/register", user);
    }

}