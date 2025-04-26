using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;
using Core;
using MongoDB.Bson;

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
        public  IEnumerable<Annonce> Get()
        {
            return annonceRepo.GetAll();
        }
        
        [HttpPost]
        public void Add(Annonce annonce)
        {
            if (string.IsNullOrEmpty(annonce.Id))
            {
                annonce.Id = ObjectId.GenerateNewId().ToString();  
            }
            annonceRepo.Add(annonce);
        }



        [HttpDelete("{id}")]
        public void DeleteById(string id)
        {
            Console.WriteLine($"Sletter annonce med id {id}");
            annonceRepo.DeleteById(id);
        }
    }
}