using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task CreateRole(string roleCode, string roleName);

        public Task<int> FindRoleByName(string roleName);
    }
}
