using MoustafaApp.Server.Service.UserService;
using System.Security.Claims;

namespace MoustafaApp.Server.Services.Common;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User =>
        _httpContextAccessor.HttpContext?.User
        ?? throw new UnauthorizedAccessException("No HttpContext found");

    public string UserId =>
      User.FindFirstValue(ClaimTypes.NameIdentifier)
      ?? throw new UnauthorizedAccessException("User ID not found");

    public string? UserName =>
        User.FindFirstValue(ClaimTypes.Name);

    public string? Email =>
        User.FindFirstValue(ClaimTypes.Email);

    public string? FullName =>
        User.FindFirstValue("fullName");

    public string? Phone =>
        User.FindFirstValue("phone");

    public IEnumerable<string> Roles =>
        User.FindAll(ClaimTypes.Role).Select(r => r.Value);
}