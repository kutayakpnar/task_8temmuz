using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRegistrationApi.Data;
using UserRegistrationApi.Models;
using UserRegistrationApi.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;

namespace UserRegistrationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


        [HttpPost("register")]
public async Task<ActionResult<User>> RegisterUser([FromForm] UserCreateDto userDto)
{
    var user = new User
    {
        Name = userDto.Name,
        Email = userDto.Email,
        Password = userDto.Password
    };

    if (userDto.ProfilePicture != null)
    {
        var uploadsFolder = Path.Combine("uploads");
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + userDto.ProfilePicture.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            userDto.ProfilePicture.CopyTo(fileStream);
        }
        user.ProfilePictureUrl = "/uploads/" + uniqueFileName;
    }

    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
}


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // PUT: api/Users/changeName/{id}
        [HttpPut("changeName/{id}")]
        public async Task<IActionResult> ChangeName(int id, ChangeNameDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = dto.NewName;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Users/changeEmail/{id}
        [HttpPut("changeEmail/{id}")]
        public async Task<IActionResult> ChangeEmail(int id, ChangeEmailDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Email = dto.NewEmail;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Users/changePassword/{id}
        [HttpPut("changePassword/{id}")]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Password = dto.NewPassword;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
