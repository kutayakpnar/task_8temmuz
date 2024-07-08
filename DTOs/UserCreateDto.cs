using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace UserRegistrationApi.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IFormFile ProfilePicture { get; set; }
    }
}