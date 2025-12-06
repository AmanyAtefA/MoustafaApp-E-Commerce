

namespace MoustafaApp.Server.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        
        [EmailAddress, Required]
        public string Email { get; set; }
       
        [Required]
        public string PhoneNumber { get; set; }
       
        [Required,]
        public string Password { get; set; }
       
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        
    }
}
