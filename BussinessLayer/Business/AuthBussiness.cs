using BussinessLayer.IBusiness;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.IRepository;
using RepositoryLayer.JwtRelated;
using RepositoryLayer.ModelDto;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Business
{
    public class AuthBussiness:IAuthBussiness
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthBussiness(IAuthRepository repo, IConfiguration config, JwtTokenGenerator jwtTokenGenerator)
        {
            _repo = repo;
            _config = config;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public string Authenticate(LoginDto model)
        {
            var user = _repo.GetUser(model.Username, model.Password);

            if (user == null)
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                //expires: DateTime.Now.AddHours(1),
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public AuthResponseDto AuthenticateWithRefresh(LoginDto model)
        {
            var user = _repo.GetUser(model.Username, model.Password);

            if (user == null)
                return null;

            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Created = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            _repo.SaveRefreshToken(refreshTokenEntity);
            _repo.SaveChanges();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }


        public AuthResponseDto RefreshToken(string refreshToken)
        {
            
            var storedToken = _repo.GetRefreshToken(refreshToken);

            // Validate token
            if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiryDate < DateTime.UtcNow)
            {
                return null; // token invalid
            }

            var user = _repo.GetUserById(storedToken.UserId);

            
            // Generate new access token
            var newAccessToken = _jwtTokenGenerator.GenerateToken(user);

            // Return the same refresh token so it can be used again
            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = refreshToken
            };
        }

    }
}
