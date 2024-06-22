using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Interface;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config, SymmetricSecurityKey key)
    {
        _config = config;
        _key = key;
    }
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.GivenName, user.UserName)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(30),
            SigningCredentials = creds,
            Issuer = _config["Issuer"],
            Audience = _config["Audience"],
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
}