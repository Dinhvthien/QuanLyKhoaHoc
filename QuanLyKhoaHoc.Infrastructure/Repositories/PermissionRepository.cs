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
    public class PermissionRepository
       : Repository<Permission>,
           IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task CreatePermission(int userId, int roleId)
        {
            Permission permission = new Permission();
            permission.UserId = userId;
            permission.RoleId = roleId;

            try
            {
                await AddAsync(permission);
            }
            catch
            {
                throw;
            }
        }
    }
}
