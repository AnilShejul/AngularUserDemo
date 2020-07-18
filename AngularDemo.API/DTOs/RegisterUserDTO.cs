using System.ComponentModel.DataAnnotations;

namespace AngularDemo.API.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="You must specfiy Password length between 4 and 8 characters.")]
        public string Password { get; set; }
    }
}