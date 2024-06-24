using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Domain.Interfaces
{
    public interface IConfirmEmailRepository : IRepository<ConfirmEmail>
    {
      public Task<bool> ConfirmEmailUseCode(int userId, string confirmCode);
    }
}
