using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyDictionary.Application.Interfaces.Services;
using MyDictionary.Domain.Modules.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyDictionary.Api.Providers;

internal class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string Create(User user)
    {
        string secretKey = configuration["Jwt:Secret"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("userId", user.Id.ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:ExipeTimeInMinites"])),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JwtSecurityTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}
