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
            // Log URL'en, der kaldes
            Console.WriteLine($"Kalder API URL: {serverUrl}api/annoncer");
        
            // Få rå respons
            var response = await client.GetAsync($"{serverUrl}api/annoncer");
        
            // Log statuskode
            Console.WriteLine($"API svarede med status: {response.StatusCode}");
        
            // Læs respons indhold som tekst
            var content = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
                
            {
                Console.WriteLine("API returnerede fejlstatus!");
                return Array.Empty<Annonce>();
            }
        
            // Forsøg at deserialisere
            return System.Text.Json.JsonSerializer.Deserialize<Annonce[]>(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af annoncer: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return Array.Empty<Annonce>();
        }
    }

    public async Task Add(Annonce annonce)
    {
        await client.PostAsJsonAsync<Annonce>($"{serverUrl}api/annoncer", annonce);
    }

    public async Task DeleteById(int id)
    {
        await client.DeleteAsync($"{serverUrl}api/annoncer/{id}");
    }
}