namespace Application.Core.Abstractions.Authentication;
public interface IJwtProvider
{
    string Create(string userId, string name);
}
