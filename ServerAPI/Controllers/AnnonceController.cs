using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

using Core;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/annoncer")]
    public class AnnonceController : ControllerBase
    {
        private IAnnonceRepository annonceRepo;

        public AnnonceController(IAnnonceRepository annonceRepo) {
            this.annonceRepo = annonceRepo;
        }

        [HttpGet]
        public IEnumerable<Annonce> Get()
        {
            return annonceRepo.GetAll();
        }
        
        [HttpPost]
        public void Add(Annonce bike) {
            annonceRepo.Add(bike);
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