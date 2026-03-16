using BussinessLayer.IBusiness;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.IRepository;
using RepositoryLayer.ModelDto;
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

        public AuthBussiness(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
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
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
    }
