using Core;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository) => _repository = repository;

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginRequest request)
        {
            var user = _repository.GetUser(request.Username, request.Password);
            return user != null ? Ok(user) : Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest("Brugernavn og adgangskode er påkrævet");
                }

                _repository.AddUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
            return user != null ? Ok(user) : NotFound();
        }
    }

    public record LoginRequest(string Username, string Password);
}