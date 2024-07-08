using System.ComponentModel.DataAnnotations;

namespace UserRegistrationApi.Dtos
{
    public class ChangeEmailDto
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
