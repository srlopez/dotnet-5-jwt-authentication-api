using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Settings;

using WebApi.Models;

namespace WebApi.Auth
{
    public interface IAuthService
    {
        AuthResponse Authenticate(AuthRequest model);
    }

    public class AuthService : IAuthService
    {
        // users list
        private List<User> _users = new List<User>
        {
            new User {  Id = 1, FirstName = "Test", LastName = "User", 
                        Role = "User",
                        Username = "test", Password = "test" },
            new User {  Id = 2, FirstName = "Santi", LastName = "Lopez", 
                        Role = "Admin",
                        Username = "santi", Password = "1234" }
        };

        private readonly AppSettings _appSettings;

        public AuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthResponse Authenticate(AuthRequest req)
        {
            var user = _users.SingleOrDefault(u => u.Username == req.Username && u.Password == req.Password);

            // 1.- control null
            if (user == null) return null;
            // 2.- control db

            // autenticacion válida -> generamos jwt
            // generamos un token válido para 7 días
            var fExpiracion = DateTime.UtcNow.AddDays(7);
            var token = generateJwtToken(user, fExpiracion);

            // Devolvemos lo que nos interese
            return new AuthResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = token,
                ValidTo = fExpiracion
            };

        }

        // internos
        private string generateJwtToken(User user, DateTime expiracion)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = expiracion,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return  tokenHandler.WriteToken(token);
        }
    }
}