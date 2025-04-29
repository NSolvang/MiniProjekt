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
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Annonce annonce)
        {
            if (id != annonce.Id)
            {
                return BadRequest("ID i URL stemmer ikke overens med ID i annoncen");
            }
    
            await annonceRepo.Update(annonce);
            return NoContent();
        }
        
        [HttpGet("buyer/{buyerId}")]
        public async Task<IActionResult> GetByBuyerId(int buyerId)
        {
            var annoncer = await annonceRepo.GetByBuyerId(buyerId);
            return Ok(annoncer);
        }
        
        //filter
        [HttpGet("filtered")]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] string? category = null, 
            [FromQuery] int? minPrice = null, 
            [FromQuery] int? maxPrice = null,
            [FromQuery] string? location = null)
        {
            var annoncer = await annonceRepo.GetFiltered(category, minPrice, maxPrice, location);
            return Ok(annoncer);
        }
    }
}