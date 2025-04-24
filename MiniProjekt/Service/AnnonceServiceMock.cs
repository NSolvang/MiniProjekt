using Core;

namespace MiniProjekt.Service;

public class AnnonceServiceMock : IAnnonceService
{
    private List<Annonce>? annonceList = new();
    
    public async Task<Annonce[]> GetAll()
    {
        return annonceList.ToArray();
    }

    public async Task Add(Annonce annonce)
    {
        int max = 0;
        if (annonceList.Count > 0)
            max = annonceList.Select(b => b.AnnonceID).Max();
        annonce.AnnonceID = max + 1;
        annonceList.Add(annonce);
    }

    public async Task DeleteById(int id)
    {
        annonceList.RemoveAll(bike => bike.AnnonceID == id);
    }
}