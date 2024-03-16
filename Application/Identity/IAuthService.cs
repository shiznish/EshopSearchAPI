using Application.Models.Identity;

namespace Application.Identity;
public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest authRequest);
    Task<RegistrationResponse> Register(RegistrationRequest resgistrationRequest);
}
