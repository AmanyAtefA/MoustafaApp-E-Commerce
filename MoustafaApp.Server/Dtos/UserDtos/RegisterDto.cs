namespace MoustafaApp.Server.Dtos.UserDtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Full name must be between 3 and 20 characters")]
        [RegularExpression(@"^[A-Za-z\s]+$",
            ErrorMessage = "Full name can contain letters and spaces only")]
        public string FullName { get; set; }
        

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
