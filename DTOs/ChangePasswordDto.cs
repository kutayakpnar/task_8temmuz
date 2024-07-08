using System.ComponentModel.DataAnnotations;

namespace UserRegistrationApi.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
