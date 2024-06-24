using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IJwtAccessTokenService
    {
        public string GenerateAccessToken(User user);
    }
}
