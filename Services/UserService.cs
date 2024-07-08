using Microsoft.EntityFrameworkCore;
using UserRegistrationApi.Data;
using UserRegistrationApi.Models;


namespace UserRegistrationApi.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> ChangeName(int id, string newName)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            user.Name = newName;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> ChangeEmail(int id, string newEmail)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            user.Email = newEmail;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> ChangePassword(int id, string newPassword)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            user.Password = newPassword;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> RegisterUser(User user, IFormFile profilePicture)
        {
            //I add user profile photos to upload file according to my decision.
            if (profilePicture != null)
            {
                var uploadsFolder = Path.Combine("uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + profilePicture.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profilePicture.CopyTo(fileStream);
                }
                user.ProfilePictureUrl = "/uploads/" + uniqueFileName;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<bool> DeleteUser(int id)
{
    var user = await _context.Users.FindAsync(id);
    if (user == null)
    {
        return false;
    }

    _context.Users.Remove(user);
    await _context.SaveChangesAsync();
    return true;
}

    }
}
