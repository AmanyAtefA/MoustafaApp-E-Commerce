

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoustafaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }


        [HttpGet("GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            try
            {
                var Roles = await _unitOfWork.RoleManager.Roles.ToListAsync();

                var RoleDto = _mapper.Map<List<RoleDto>>(Roles);

                return Ok(RoleDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetAllUsersWithRolls")]
        public async Task<ActionResult> GetAllUsersWithRolls()
        {
            try
            {
                var Users = await _unitOfWork.UserManager.Users.ToListAsync();
                var UserWithRoles = _mapper.Map<List<UserDto>>(Users);

                for (int i = 0; i < Users.Count; i++)
                {
                    var roles = await _unitOfWork.UserManager.GetRolesAsync(Users[i]);
                    UserWithRoles[i].Roles = roles.ToList();

                    //UserWithRoles[i].IsActive =
                    //    Users[i].LockoutEnabled == false ||
                    //    Users[i].LockoutEnd == null ||
                    //    Users[i].LockoutEnd <= DateTimeOffset.Now;
                }

                return Ok(UserWithRoles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }



        [HttpGet("IsExistEmail/{Email}")]
        public async Task<ActionResult<bool>> CheckEmailExists(string Email)
        {
            var ExistEmail = await _unitOfWork.UserManager.FindByEmailAsync(Email);
            if (ExistEmail != null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet("IsExistUserName/{UserName}")]
        public async Task<ActionResult<bool>> CheckUserNameExists(string UserName)
        {

            var ExistUserName = await _unitOfWork.UserManager.FindByNameAsync(UserName);
            if (ExistUserName != null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet("IsExistPhoneNo/{PhoneNo}")]
        public async Task<ActionResult<bool>> CheckPhoneNoExists(string PhoneNo)
        {
            try
            {

                var ExistPhone = await _unitOfWork.UserManager.Users.AnyAsync(y => y.PhoneNumber == PhoneNo);
                if (ExistPhone != null)
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetUserByUserNam/{UserName}")]
        public async Task<ActionResult> GetUserByUserNam(string UserName)
        {
            var User = await _unitOfWork.UserManager.FindByNameAsync(UserName);

            if (User == null)
                return NotFound(new { message = $"User with UserName '{UserName}' not found." });

            var roles = await _unitOfWork.UserManager.GetRolesAsync(User);

            var UserWithRole = _mapper.Map <UserDto>(User);
            UserWithRole.Roles = roles.ToList();

            return Ok(UserWithRole);
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromQuery] string userName)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(userName);

            if (user == null)
                return NotFound(new { message = $"User '{userName}' not found." });

            var result = await _unitOfWork.UserManager.DeleteAsync(user);

            if (result.Succeeded)
                return Ok(new { message = $"User '{userName}' has been deleted." });

            else
                return BadRequest(new { message = "Error in delete user" });
        }



        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterDto dtoRegister) 
        {
            try
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PhoneExit = await _unitOfWork.UserManager.Users.AnyAsync(y => y.PhoneNumber == dtoRegister.PhoneNumber);
            var NameExit = await _unitOfWork.UserManager.FindByNameAsync(dtoRegister.UserName);
            var ExistEmail = await _unitOfWork.UserManager.FindByEmailAsync(dtoRegister.Email);

            if (ExistEmail != null)
            {
                return Ok(new { message = "Email already exists" });
            }

            if (NameExit != null )
            {
                return Ok(new { message = "UserName already exists" });
            }

            if (PhoneExit == true)
            {
                return Ok(new { message = " Phone Number already exists" });
            }

                var User = new ApplicationUser
                {
                    UserName = dtoRegister.UserName,
                    Email = dtoRegister.Email,
                    PhoneNumber = dtoRegister.PhoneNumber,
                };
                var result = await _unitOfWork.UserManager.CreateAsync(User, dtoRegister.Password);

                if (result.Succeeded)
                {
                    await _unitOfWork.UserManager.AddToRoleAsync(User, "User");
                    return Ok(new { message = "User registered successfully" });
                }
                return BadRequest(result.Errors);
  
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message }); 
            } 
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dtoLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid data submitted", errors = ModelState });
                }

                var user = await _unitOfWork.UserManager.FindByNameAsync(dtoLogin.UserName);

                if (user == null)
                {
                    return BadRequest(new { message = "Username is invalid" });
                }

                var passwordValid = await _unitOfWork.UserManager.CheckPasswordAsync(user, dtoLogin.Password);

                if (!passwordValid)
                {
                    return Unauthorized(new { message = "Invalid Password" });
                }

                var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
                var token = await GenerateJwtToken(user.Id);

                return Ok(new
                {
                    token = token,
                    roles = roles.ToList()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }


        private async Task<string> GenerateJwtToken(string userId)
        {
            var User = await _unitOfWork.UserManager.FindByIdAsync(userId);
            var roles = await _unitOfWork.UserManager.GetRolesAsync(User);

            var claims = new List<Claim>
            {
              new Claim("userName", User.UserName),
              new Claim("userId", User.Id),
              new Claim("email", User.Email),
              new Claim("phone", User.PhoneNumber),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiresInMinutes = int.Parse(_configuration["JWT:Expires"]);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //[Authorize(Roles = "Manager")]
        [HttpPost("AddRoll")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleDto dto)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return NotFound("User not found");

            if (!await _unitOfWork.RoleManager.RoleExistsAsync(dto.Role))
                return BadRequest("Role does not exist");

            var result = await _unitOfWork.UserManager.AddToRoleAsync(user, dto.Role);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = $"Role 'Admin' added to user '{user.UserName}'" });
        }
        
        //[Authorize(Roles = "Manager")]
        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromQuery] string userName, [FromQuery] string role)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
            if (user == null)
                return NotFound("User not found");

            if (!await _unitOfWork.RoleManager.RoleExistsAsync(role))
                return BadRequest("Role does not exist");

            if (!await _unitOfWork.UserManager.IsInRoleAsync(user, role))
                return BadRequest("User not have this role");

            var result = await _unitOfWork.UserManager.RemoveFromRoleAsync(user,role);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = $"Role '{role}' Deleted From '{user.UserName}'" });
        }

        //[Authorize]
        [HttpPut("UpdateProfileData")]
        public async Task<IActionResult> UpdateProfileData(UpdateUserDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("User not found");

            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.UserName = dto.UserName;

            var result = await _unitOfWork.UserManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);
 
            var newToken = await GenerateJwtToken(user.Id);

            return Ok(new
            {
                token = newToken
            });
        }


    }
}
