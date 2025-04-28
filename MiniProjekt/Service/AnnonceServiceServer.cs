using System.Net.Http.Json;
using Core;

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

 
}