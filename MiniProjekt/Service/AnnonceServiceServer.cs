using System.Net.Http.Json;
using Core;

namespace MiniProjekt.Service;

public class AnnonceServiceServer : IAnnonceService
{
    private string serverUrl = "http://localhost:5238/";
    
    private HttpClient client;

    private AnnonceServiceServer(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<Annonce[]> GetAll()
    {
        return await client.GetFromJsonAsync<Annonce[]>($"{serverUrl}/api/bike");
    }

    public async Task Add(Annonce annonce)
    {
        await client.PostAsJsonAsync<Annonce>($"{serverUrl}/api/bike", annonce);
    }

    public async Task DeleteById(int id)
    {
        await client.DeleteAsync($"{serverUrl}/api/bike/{id}");
    }
}