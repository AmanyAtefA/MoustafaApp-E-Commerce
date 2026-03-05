namespace MoustafaApp.Server.Service.UserService
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string? Email { get; }
        string? UserName { get; }
        string? FullName { get; }
        string? Phone { get; }
        IEnumerable<string> Roles { get; }
    }
}
