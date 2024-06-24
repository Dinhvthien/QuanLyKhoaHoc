using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Services.JWTSerVices
{
    public class JwtAccessTokenService : IJwtAccessTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtAccessTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.FullName));
            // Add Role
            if (user.Permissions != null)
            {
                claims.AddRange(
                    user.Permissions.Where(x => x.UserId == user.Id)
                        .Select(x => new Claim(
                            ClaimTypes.Role,
                            x.Role != null ? (x.Role.RoleName) : ""
                        ))
                );
            }

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!)
                    ),
                    SecurityAlgorithms.HmacSha256Signature
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
