using Application.Core.Abstractions.Authentication;
using Application.Core.Abstractions.Common;
using Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication;
internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTime _dateTime;

    public JwtProvider(
          IOptions<JwtSettings> jwtOptions,
          IDateTime dateTime)
    {
        _jwtSettings = jwtOptions.Value;
        _dateTime = dateTime;
    }
    public string Create(string userId, string name)
    {
        return "";
    }
}
