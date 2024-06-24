using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Domain.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        public Task AddNewRefreshToken(
            string token,
            DateTime expiryTime,
            int userId
        );
    }
}
