namespace MoustafaApp.Server.Dtos.UserDtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public List<string> Roles { get; set; }

    }

}
