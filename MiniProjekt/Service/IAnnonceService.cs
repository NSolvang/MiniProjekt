using Core;
namespace MiniProjekt.Service;

public interface IAnnonceService
{
    Task<Annonce[]> GetAll();
    Task Add(Annonce annonce);
    Task DeleteById(string id);
    Task Update(Annonce annonce);
    
}