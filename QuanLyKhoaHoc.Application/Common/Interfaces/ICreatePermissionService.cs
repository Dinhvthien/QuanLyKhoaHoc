using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface ICreatePermissionService
    {
        public Task<bool> NewDefaultPermission(int userId);
        public Task<bool> CreatePermission(int userId, int role);
        public Task<bool> UpdatePermission(int permissionid , int userId, int role);
        public Task<bool> DeletePermission(int userId, int role);
        public Task<IEnumerable<Permission>> GetPermission();
    }
}
