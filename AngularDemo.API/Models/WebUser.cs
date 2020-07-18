namespace AngularDemo.API.Models
{
    public class WebUser
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}