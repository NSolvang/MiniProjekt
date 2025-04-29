using System.Net.Http.Json;
using Core;
using MongoDB.Driver;

namespace MiniProjekt.Service;

public class AnnonceServiceServer : IAnnonceService
{
    private string serverUrl = "http://localhost:5063/";
    
    private HttpClient client;
    
    public AnnonceServiceServer(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<Annonce[]> GetAll()
    {
        try
        {
            var response = await client.GetAsync($"{serverUrl}api/annoncer");
        
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<Annonce>();
            }
        
            var content = await response.Content.ReadAsStringAsync();
        
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        
            return System.Text.Json.JsonSerializer.Deserialize<Annonce[]>(content, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af annoncer: {ex.Message}");
            return Array.Empty<Annonce>();
        }
    }

    public async Task Add(Annonce annonce)
    {
        
        await client.PostAsJsonAsync<Annonce>($"{serverUrl}api/annoncer", annonce);

    }

    public async Task DeleteById(string id)
    {
        Console.WriteLine($"Sletter annonce med ID: {id}");
        await client.DeleteAsync($"{serverUrl}api/annoncer/{id}");

    }

    public async Task Update(Annonce annonce)
    {
        try
        {
            await client.PutAsJsonAsync($"{serverUrl}api/annoncer/{annonce.Id}", annonce);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af annonce: {ex.Message}");
        }
    }
    
    public async Task<Annonce[]> GetByBuyerId(int buyerId)
    {
        try
        {
            var response = await client.GetAsync($"{serverUrl}api/annoncer/buyer/{buyerId}");
        
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<Annonce>();
            }
        
            var content = await response.Content.ReadAsStringAsync();
        
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        
            return System.Text.Json.JsonSerializer.Deserialize<Annonce[]>(content, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af køberens annoncer: {ex.Message}");
            return Array.Empty<Annonce>();
        }
    }
    
    public async Task<Annonce[]> GetFiltered(string? category = null, int? minPrice = null, int? maxPrice = null, string? location = null)
    {
        try
        {
            // Byg URL med query parametre
            var queryParams = new List<string>();
        
            if (!string.IsNullOrEmpty(category))
                queryParams.Add($"category={Uri.EscapeDataString(category)}");
        
            if (minPrice.HasValue)
                queryParams.Add($"minPrice={minPrice.Value}");
        
            if (maxPrice.HasValue)
                queryParams.Add($"maxPrice={maxPrice.Value}");
        
            if (!string.IsNullOrEmpty(location))
                queryParams.Add($"location={Uri.EscapeDataString(location)}");

            var queryString = queryParams.Count > 0 ? $"?{string.Join("&", queryParams)}" : "";
        
            var response = await client.GetAsync($"{serverUrl}api/annoncer/filtered{queryString}");
        
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Fejl ved filtrering af annoncer: {response.StatusCode}");
                return Array.Empty<Annonce>();
            }
        
            var content = await response.Content.ReadAsStringAsync();
        
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        
            return System.Text.Json.JsonSerializer.Deserialize<Annonce[]>(content, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved filtrering af annoncer: {ex.Message}");
            return Array.Empty<Annonce>();
        }
    }

}