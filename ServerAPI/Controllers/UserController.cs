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

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginRequest request)
        {
            var user = _repository.GetUser(request.Username, request.Password);
            if (user == null) return NotFound("Invalid credentials");
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            _repository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.ID }, user);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
}