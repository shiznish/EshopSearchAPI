using Application.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.Services;
public sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private List<Claim> _permissions;
    public CurrentUser(IHttpContextAccessor httpContextAccessor,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _roleManager = roleManager;
        UserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        UserName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        UserFullName = httpContextAccessor.HttpContext?.User?.FindFirst("FullName")?.Value;
        Roles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(x => x.Value.ToString()).ToList();
    }
    public string UserId { get; }
    public string UserName { get; }
    public string? UserFullName { get; }
    public IReadOnlyList<string> Roles { get; }
}
