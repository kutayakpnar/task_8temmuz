using Microsoft.AspNetCore.Mvc;
using UserRegistrationApi.Services;
using UserRegistrationApi.Dtos;
using UserRegistrationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserRegistrationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromForm] UserCreateDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password
            };

            var createdUser = await _userService.RegisterUser(user, userDto.ProfilePicture);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/Users/changeName/{id}
        [HttpPut("changeName/{id}")]
        public async Task<IActionResult> ChangeName(int id, ChangeNameDto dto)
        {
            var user = await _userService.ChangeName(id, dto.NewName);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // PUT: api/Users/changeEmail/{id}
        [HttpPut("changeEmail/{id}")]
        public async Task<IActionResult> ChangeEmail(int id, ChangeEmailDto dto)
        {
            var user = await _userService.ChangeEmail(id, dto.NewEmail);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // PUT: api/Users/changePassword/{id}
        [HttpPut("changePassword/{id}")]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordDto dto)
        {
            var user = await _userService.ChangePassword(id, dto.NewPassword);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
