using Application.Identity;
using Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AcoountController : ControllerBase
{
    private readonly IAuthService _accountService;
    public AcoountController(IAuthService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        return Ok(await _accountService.Login(request, GenerateIPAddress()));
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegistrationRequest request)
    {
        return Ok(await _accountService.Register(request));
    }

    private string GenerateIPAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }

}
