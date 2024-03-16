using Application.Identity;
using Application.Models.Identity;
using Identity.Models;
using Infrastructure.Authentication.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettins;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettins, SignInManager<ApplicationUser> signInManager)
    {
        this._userManager = userManager;
        this._jwtSettins = jwtSettins.Value;
        this._signInManager = signInManager;
    }
    public async Task<AuthResponse> Login(AuthRequest authRequest, string ipAddress)
    {
        var user = await _userManager.FindByEmailAsync(authRequest.Email);
        if (user == null)
        {
            throw new Exception($"User not found with {authRequest.Email}");
        }
        var result = await _signInManager.PasswordSignInAsync(user.UserName, authRequest.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new Exception($"User not found with {authRequest.Email}");
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
        AuthResponse response = new AuthResponse
        {
            Id = user.Id,
            token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };
        return response;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
        if (userWithSameUserName != null)
        {
            throw new Exception($"Username '{request.UserName}' is already taken.");
        }
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName
        };
        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithSameEmail == null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                throw new Exception($"Errors '{result.Errors}'");
            }
        }
        else
        {
            throw new Exception($"Email '{request.Email}' already exists");
        }
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim("uid",user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettins.Key));
        var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
          issuer: _jwtSettins.Issuer,
          audience: _jwtSettins.Audience,
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(_jwtSettins.DurationInMinutes),
          signingCredentials: signInCredentials);

        return jwtSecurityToken;
    }
}
