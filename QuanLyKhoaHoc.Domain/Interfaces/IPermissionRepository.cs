using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Domain.Interfaces
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        public Task CreatePermission(int userId, int roleId);
    }
}
