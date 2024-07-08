using System.ComponentModel.DataAnnotations;

namespace UserRegistrationApi.Dtos
{
    public class ChangeNameDto
    {
        [Required]
        public string NewName { get; set; }
    }
}
