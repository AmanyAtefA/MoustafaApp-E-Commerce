namespace MoustafaApp.Server.Dtos.UserDtos
{
    public class LoginDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
