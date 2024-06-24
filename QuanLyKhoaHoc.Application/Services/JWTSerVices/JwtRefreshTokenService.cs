using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Services.JWTSerVices
{
    public class JwtRefreshTokenService : IJwtRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public JwtRefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task LoginCreateRefreshToken(string token, DateTime expiryTime, int userId)
        {
            await _refreshTokenRepository.AddNewRefreshToken(
            token,
            expiryTime,
            userId
        );
        }
    }
}
