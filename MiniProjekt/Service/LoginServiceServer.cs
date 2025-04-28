using Core;
using System.Net.Http.Json;
using MiniProjekt.Service;
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

    public async Task<User?> GetUserLoggedIn()
    {
        var res = await localStorage.GetItemAsync<User>("user");
        return res;
    }

    public async Task<bool> Login(string UserName, string Password)
    {
        try
        {
            // Create login request object
            var loginRequest = new LoginRequest
            {
                Username = UserName,
                Password = Password
            };

            // Send POST request with JSON body
            var response = await client.PostAsJsonAsync($"{serverUrl}api/User/login", loginRequest);

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
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}");
            return false;
        }
    }

    public async Task AddUser(User user)
    {
        await client.PostAsJsonAsync($"{serverUrl}api/User", user);
    }
}


public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}