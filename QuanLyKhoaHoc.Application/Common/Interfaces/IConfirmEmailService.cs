using QuanLyKhoaHoc.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IConfirmEmailService
    {
        public Task<string> CreateConfirmEmail(int userId);

        public Task<Result> UserConfirmEmailUseCode(int userId, string confirmCode);
    }
}
