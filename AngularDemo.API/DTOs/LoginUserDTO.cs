using System.ComponentModel.DataAnnotations;

namespace AngularDemo.API.DTOs
{
    public class LoginUserDTO
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}