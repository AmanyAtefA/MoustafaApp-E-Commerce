

using Microsoft.IdentityModel.Tokens;
using MoustafaApp.Server.Dtos.UserDtos;
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
                var users = await _unitOfWork.UserManager.Users.ToListAsync();

                var result = new List<UserDto>();

                foreach (var user in users)
                {
                    var dto = _mapper.Map<UserDto>(user);
                    dto.Roles = (await _unitOfWork.UserManager.GetRolesAsync(user)).ToList();
                    result.Add(dto);
                }
            
                return Ok(result);
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

                bool Exists = await _unitOfWork.UserManager.Users.AnyAsync(y => y.PhoneNumber == PhoneNo);
                return Ok(Exists);
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



        private async Task<string> GenerateUsername(string fullName)
        {
            var baseUsername = fullName.Replace(" ", "").ToLower();
            var username = baseUsername;
            int counter = 1;

            while (await _unitOfWork.UserManager.FindByNameAsync(username) != null)
            {
                username = baseUsername + counter;
                counter++;
            }

            return username;
        }



        
        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterDto dtoRegister)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _mapper.Map<ApplicationUser>(dtoRegister);

                user.UserName = await GenerateUsername(dtoRegister.UserName);

                if (await _unitOfWork.UserManager.FindByEmailAsync(dtoRegister.Email) != null)
                    return BadRequest("Email already exists");

                if (await _unitOfWork.UserManager.FindByNameAsync(user.UserName) != null)
                    return BadRequest("Username already exists");

                if (await _unitOfWork.UserManager.Users
                    .AnyAsync(x => x.PhoneNumber == dtoRegister.PhoneNumber))
                    return BadRequest("Phone number already exists");

                var result = await _unitOfWork.UserManager.CreateAsync(user, dtoRegister.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                await _unitOfWork.UserManager.AddToRoleAsync(user, "User");

                return Ok(new
                {
                    message = "User registered successfully",
                    username = user.UserName
                });
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


                var user = await _unitOfWork.UserManager.FindByEmailAsync(dtoLogin.Email.Trim().ToLower());


                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }
                
                var passwordValid = await _unitOfWork.UserManager.CheckPasswordAsync(user, dtoLogin.Password);

                if (!passwordValid)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
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
              new Claim(ClaimTypes.NameIdentifier, User.Id),

              new Claim(ClaimTypes.Name, User.UserName),
              new Claim(ClaimTypes.Email, User.Email),
              
              new Claim("fullName", User.FullName ?? ""),
              new Claim("phone", User.PhoneNumber ?? ""),


              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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
            user.FullName = dto.FullName;

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
