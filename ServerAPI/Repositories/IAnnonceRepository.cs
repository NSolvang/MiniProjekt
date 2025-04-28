using Core;
namespace ServerAPI.Repositories;

public interface IAnnonceRepository
{
    Annonce[] GetAll();
    void Add(Annonce annonce);
    void DeleteById(string id);
    
    Task Update(Annonce annonce);
    
    Task<Annonce[]> GetByBuyerId(int buyerId);

}