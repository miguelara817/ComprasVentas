using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ComprasVentas.Common;
using ComprasVentas.Models;
using ComprasVentas.Services.spec;
using Microsoft.IdentityModel.Tokens;

namespace ComprasVentas.Services.impl;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }
    public string GenerateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Name, usuario.Nombre)
            // Add data to claims as needed  
        };

        // add roles y permisos
        foreach (var role in usuario.Roles)
        {   
            claims.Add(new Claim(ClaimTypes.Role, role.Nombre));

            // add permisos
            foreach (var permiso in role.Permisos)
            {
                claims.Add(new Claim("Permiso", permiso.Nombre));
            }
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }


    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        throw new NotImplementedException();
    }

    public DateTime GetTokenExpiration()
    {
        throw new NotImplementedException();
    }
}
