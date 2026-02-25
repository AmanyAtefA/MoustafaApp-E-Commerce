using MoustafaApp.Server.Dtos.CartDtos;

namespace MoustafaApp.Server.Dtos.UserDtos
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public List<string> Roles { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<CartDto> Carts { get; set; }

    }

}
