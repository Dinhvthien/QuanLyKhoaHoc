using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IJwtRefreshTokenService
    {
        public string GenerateRefreshToken();
        public Task LoginCreateRefreshToken(
            string token,
            DateTime expiryTime,
            int userId
        );
    }
}
