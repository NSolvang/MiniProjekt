using Core;
namespace ServerAPI.Repositories;

public interface IAnnonceRepository
{
    IEnumerable<Annonce> GetAll();
    Annonce GetById(int id);
    void Add(Annonce annonce);
    void DeleteById(int id);
}