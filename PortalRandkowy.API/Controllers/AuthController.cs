using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
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
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            userForRegister.UserName = userForRegister.UserName.ToLower();

            if (await _repository.UserExists(userForRegister.UserName))
                return BadRequest("Użytkownik o takiej nazwie już istnieje!");

            var userToCreate = new User 
            {
                Username = userForRegister.UserName
            };
            
            var createdUser = await _repository.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);
        }
        
    }
}