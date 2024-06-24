using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.Interfaces;
using QuanLyKhoaHoc.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Infrastructure.Repositories
{
    public class RefreshTokenRepository
     : Repository<RefreshToken>,
         IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task AddNewRefreshToken(
            string token,
            DateTime expiryTime,
            int userId
        )
        {
            RefreshToken refreshToken = new RefreshToken()
            {
                Token = token,
                ExpiryTime = expiryTime,
                UserId = userId
            };

            try
            {
                await AddAsync(refreshToken);
            }
            catch
            {
                throw;
            }
        }
    }
}