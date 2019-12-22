using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            // Validate Request
            username = username.ToLower();
            if(await _repository.UserExists(username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = username
            };

            var createdUser = await _repository.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}