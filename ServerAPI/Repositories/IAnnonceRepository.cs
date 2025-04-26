using Core;
namespace ServerAPI.Repositories;

public interface IAnnonceRepository
{
    Annonce[] GetAll();
    void Add(Annonce annonce);
    void DeleteById(string id);
    
    void Update(Annonce annonce);
}