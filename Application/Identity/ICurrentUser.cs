namespace Application.Identity;
public interface ICurrentUser
{
    string UserId { get; }
    string UserName { get; }
    string UserFullName { get; }
    IReadOnlyList<string> Roles { get; }
}