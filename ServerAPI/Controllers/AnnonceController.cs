using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

using Core;
namespace ServerAPI.Controllers;

public class AnnonceController
{
    [ApiController]
    [Route("api/annoncer")]
    public class BikeController : ControllerBase
    {
        private IAnnonceRepository annonceRepo;

        public BikeController(IAnnonceRepository annonceRepo) {
            this.annonceRepo = annonceRepo;
        }

        [HttpGet]
        public IEnumerable<Annonce> Get()
        {
            return annonceRepo.GetAll();
        }
        
        [HttpPost]
        public void Add(Annonce annonce) {
            annonceRepo.Add(annonce);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void DeleteById(int id)
        {
            Console.WriteLine($"Sletter annonce med id {id}");
            annonceRepo.DeleteById(id);
        }
    }
}