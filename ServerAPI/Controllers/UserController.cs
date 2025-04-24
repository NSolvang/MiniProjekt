using Core;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase  // Changed from 'Controllers' to 'ControllerBase'
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)  // Changed parameter name from UserID to id to match route
        {
            var user = _repository.GetById(id);  // Changed variable name from product to user
            if (user == null) return NotFound($"User with id {id} not found");
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            _repository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, user);
        }
    }
}
