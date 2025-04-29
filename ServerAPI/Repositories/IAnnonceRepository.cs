using Core;
namespace ServerAPI.Repositories;

public interface IAnnonceRepository
{
    Annonce[] GetAll();
    void Add(Annonce annonce);
    void DeleteById(string id);
    
    Task Update(Annonce annonce);
    
    Task<Annonce[]> GetByBuyerId(int buyerId);

    // Opdatering af filtermetoden til at inkludere location
    Task<Annonce[]> GetFiltered(string? category = null, int? minPrice = null, int? maxPrice = null, string? location = null);
}