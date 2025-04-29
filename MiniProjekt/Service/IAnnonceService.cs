using Core;

namespace MiniProjekt.Service;

public interface IAnnonceService
{
    Task<Annonce[]> GetAll();
    Task Add(Annonce annonce);
    Task DeleteById(string id);
    Task Update(Annonce annonce);
    
    // Ny metode til filtrering, nu med Location
    Task<Annonce[]> GetFiltered(string? category = null, int? minPrice = null, int? maxPrice = null, string? location = null);
}