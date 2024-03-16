using Application.Models.Identity;

namespace Application.Identity;
public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest authRequest, string ipAddress);
    Task<RegistrationResponse> Register(RegistrationRequest resgistrationRequest);
}
